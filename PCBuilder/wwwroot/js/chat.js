document.addEventListener('DOMContentLoaded', function () {
    const messagesContainer = document.getElementById('chatMessages');
    const form = document.getElementById('chatForm');
    const input = document.getElementById('userMessageInput');
    const loading = document.getElementById('chatLoading');
    const sendButton = document.getElementById('sendButton');

    if (!form || !input || !messagesContainer) {
        console.error("Chat elements not found!");
        return;
    }

    console.log("Chat.js successfully loaded!");

    // Автоувеличение textarea
    input.addEventListener('input', function () {
        this.style.height = 'auto';
        this.style.height = Math.min(this.scrollHeight, 120) + 'px';
    });

    form.addEventListener('submit', async function (e) {
        e.preventDefault();

        const message = input.value.trim();
        if (!message) return;
        if (loading.classList.contains('d-none') === false) return; // уже отправляется

        // Добавляем сообщение пользователя
        addMessage('user', message);
        input.value = '';
        input.style.height = 'auto';

        await sendToServer(message);
    });

    async function sendToServer(message) {
        loading.classList.remove('d-none');
        sendButton.disabled = true;

        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value || '';

            const formData = new FormData();
            formData.append("message", message);

            console.log("Отправка запроса на /Chat?handler=Send...");

            const response = await fetch('/Chat?handler=Send', {
                method: 'POST',
                headers: {
                    'RequestVerificationToken': token
                },
                body: formData
            });

            console.log("Статус ответа:", response.status);

            const data = await response.json();
            console.log("Ответ от сервера:", data);

            if (data.ok && data.response) {
                addMessage('ai', data.response);
            } else {
                addMessage('ai', `Ошибка: ${data.error || 'Неизвестная ошибка'}`);
            }
        } catch (error) {
            console.error("Ошибка при отправке:", error);
            addMessage('ai', 'Ошибка соединения с сервером. Проверь консоль (F12).');
        } finally {
            loading.classList.add('d-none');
            sendButton.disabled = false;
        }
    }

    function addMessage(role, content) {
        const div = document.createElement('div');
        div.className = `chat-message ${role}`;

        const isUser = role === 'user';
        div.innerHTML = `
            <div class="chat-message-avatar">
                <i class="bi ${isUser ? 'bi-person-circle' : 'bi-robot'}"></i>
            </div>
            <div class="chat-message-content">
                ${typeof content === 'string'
                ? content.replace(/\n/g, '<br>')
                : formatAIResponse(content)}
            </div>
        `;

        messagesContainer.appendChild(div);
        messagesContainer.scrollTop = messagesContainer.scrollHeight;
    }

    function formatAIResponse(data) {
        if (!data.buildName) return `<strong>Не удалось обработать ответ</strong>`;

        let html = `<strong>${data.buildName}</strong><br><br>`;
        html += `<small>${data.reasoning || ''}</small><br><br>`;

        if (data.components && data.components.length > 0) {
            data.components.forEach(c => {
                html += `<b>${c.category}:</b> ${c.name} — ${Number(c.price).toLocaleString('ru-RU')} ₽<br>`;
            });
        }

        html += `<hr><b>Итого:</b> ${Number(data.totalPrice).toLocaleString('ru-RU')} ₽`;
        return html;
    }

    // Приветственное сообщение
    setTimeout(() => {
        if (messagesContainer.children.length === 0) {
            addMessage('ai', 'Привет! Опиши, какой ПК тебе нужен (бюджет, задачи — игры, работа, монтаж и т.д.), и я сделаю сборку.');
        }
    }, 600);
});