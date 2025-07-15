document.addEventListener('DOMContentLoaded', () => {
    function initializeAutoComplete(inputId, hiddenFieldId, dataList, resultListId) {
        const input = document.querySelector(`#${inputId}`);
        const hiddenField = document.getElementById(hiddenFieldId);
        const resultList = document.querySelector(`#${resultListId}`);
        let currentFocus = -1;  // Controla a posição do item selecionado

        input.addEventListener("input", () => {
            const query = input.value.toLowerCase();
            resultList.innerHTML = '';
            currentFocus = -1;  // Reinicia o índice de seleção

            if (query.trim() === '') {
                hiddenField.value = '';
            }

            if (query.length >= 3) {
                //console.log("DataList:", dataList); // Verifica o que está chegando na função
                //if (!Array.isArray(dataList)) {
                //    console.error("Erro: dataList não é um array", dataList);
                //    return;
                //}
                //console.log("Primeiro item do dataList:", dataList[0]);
                const matches = dataList
                    .filter(item => item.Text) // Remove itens onde Text é null ou undefined
                    .filter(item => item.Text.toLowerCase().includes(query));

               /* const matches = dataList.filter(item => item.Text.toLowerCase().includes(query));*/
                resultList.style.display = matches.length ? 'block' : 'none';

                matches.forEach(match => {
                    const listItem = document.createElement("li");
                    listItem.textContent = match.Text;
                    listItem.dataset.id = match.Value;

                    listItem.addEventListener('click', () => {
                        input.value = match.Text;
                        hiddenField.value = match.Value;
                        resultList.style.display = 'none';
                    });

                    resultList.appendChild(listItem);
                });
            } else {
                resultList.style.display = 'none';
            }
        });

        // Adiciona o evento de navegação pelo teclado
        input.addEventListener("keydown", (e) => {
            const items = resultList.querySelectorAll("li");
            if (resultList.style.display === 'block') {
                if (e.key === "ArrowDown") {
                    // Mover para o próximo item
                    currentFocus++;
                    highlightActive(items);
                } else if (e.key === "ArrowUp") {
                    // Mover para o item anterior
                    currentFocus--;
                    highlightActive(items);
                } else if (e.key === "Enter") {
                    // Selecionar o item atual
                    e.preventDefault();
                    if (currentFocus > -1 && items[currentFocus]) {
                        items[currentFocus].click();
                    }
                }
            }
        });

        function highlightActive(items) {
            // Remove o destaque de todos os itens
            items.forEach(item => item.classList.remove("autocomplete-active"));
            if (currentFocus >= items.length) currentFocus = 0;
            if (currentFocus < 0) currentFocus = items.length - 1;
            items[currentFocus].classList.add("autocomplete-active");
        }

        document.addEventListener('click', (event) => {
            if (!event.target.closest(`#${inputId}`) && !event.target.closest(`#${resultListId}`)) {
                resultList.style.display = 'none';
            }
        });
    }

    window.initializeAutoComplete = initializeAutoComplete;
});
