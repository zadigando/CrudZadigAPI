const API_URL = "http://localhost:5228/api/produtos";

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("produto-form").addEventListener("submit", async function (event) {
        event.preventDefault();

        const nome = document.getElementById("nome").value.trim();
        const descricao = document.getElementById("descricao").value.trim();
        const preco = parseFloat(document.getElementById("preco").value);
        const quantidade = parseInt(document.getElementById("quantidade").value);

        if (!nome || !descricao || isNaN(preco) || preco <= 0 || isNaN(quantidade) || quantidade < 0) {
            alert("Preencha todos os campos corretamente!");
            return;
        }

        const produto = { nome, descricao, preco, quantidade };

        try {
            const resposta = await fetch(API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(produto)
            });

            if (!resposta.ok) {
                const erroTexto = await resposta.text();
                throw new Error(`Erro ${resposta.status}: ${erroTexto}`);
            }

            alert("Produto adicionado com sucesso!");

            this.reset();
            carregarProdutos();

        } catch (error) {
            console.error("Erro ao adicionar produto:", error);
            alert(`Houve um problema ao adicionar o produto: ${error.message}`);
        }
    });

    carregarProdutos();
});

async function carregarProdutos() {
    try {
        const resposta = await fetch(API_URL);
        if (!resposta.ok) {
            throw new Error(`Erro ${resposta.status}: Não foi possível carregar os produtos.`);
        }

        const produtos = await resposta.json();

        const tabela = document.getElementById("tabela-produtos");
        tabela.innerHTML = "";

        if (produtos.length === 0) {
            tabela.innerHTML = `<tr><td colspan="6" style="text-align:center;">Nenhum produto cadastrado</td></tr>`;
            return;
        }

        produtos.forEach(produto => {
            const rowHTML = `
                <tr>
                    <td>${produto.id}</td>
                    <td><input type="text" value="${produto.nome}" id="nome-${produto.id}"></td>
                    <td><input type="text" value="${produto.descricao}" id="desc-${produto.id}"></td>
                    <td><input type="number" value="${produto.preco}" id="preco-${produto.id}" step="0.01"></td>
                    <td><input type="number" value="${produto.quantidade}" id="quant-${produto.id}"></td>
                    <td>
                        <button onclick="atualizarProduto(${produto.id})">Salvar</button>
                        <button onclick="deletarProduto(${produto.id})">Excluir</button>
                    </td>
                </tr>
            `;
            tabela.insertAdjacentHTML("beforeend", rowHTML);
        });

    } catch (error) {
        console.error("Erro ao carregar produtos:", error);
        alert(`Erro ao atualizar a lista de produtos: ${error.message}`);
    }
}




async function atualizarProduto(id) {
    const nome = document.getElementById(`nome-${id}`).value.trim();
    const descricao = document.getElementById(`desc-${id}`).value.trim();
    const preco = parseFloat(document.getElementById(`preco-${id}`).value);
    const quantidade = parseInt(document.getElementById(`quant-${id}`).value);

    if (!nome || !descricao || isNaN(preco) || preco <= 0 || isNaN(quantidade) || quantidade < 0) {
        alert("Preencha todos os campos corretamente!");
        return;
    }

    const produto = { id, nome, descricao, preco, quantidade };

    try {
        const resposta = await fetch(`${API_URL}/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(produto)
        });

        if (!resposta.ok) throw new Error("Erro ao atualizar produto!");

        alert("Produto atualizado com sucesso!");
        carregarProdutos();
    } catch (error) {
        console.error("Erro ao atualizar produto:", error);
        alert("Erro ao atualizar produto. Verifique a conexão com a API.");
    }
}

async function deletarProduto(id) {
    if (!confirm("Tem certeza que deseja excluir este produto?")) return;

    try {
        const resposta = await fetch(`${API_URL}/${id}`, { method: "DELETE" });

        if (!resposta.ok) throw new Error("Erro ao excluir produto!");

        alert("Produto excluído com sucesso!");
        carregarProdutos();
    } catch (error) {
        console.error("Erro ao excluir produto:", error);
        alert("Erro ao excluir produto. Verifique a conexão com a API.");
    }
}

document.addEventListener("DOMContentLoaded", carregarProdutos);
