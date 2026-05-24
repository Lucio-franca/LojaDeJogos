"use strict";
const API = "/api/loja-de-jogos";
function mostrarAlerta(msg, tipo) {
    const el = document.getElementById("alerta");
    el.className = `alert alert-${tipo}`;
    el.textContent = msg;
    setTimeout(() => el.className = "alert d-none", 5000);
}
async function carregarJogos() {
    const tbody = document.getElementById("tabela");
    tbody.innerHTML = '<tr><td colspan="9" class="text-center">Carregando...</td></tr>';
    try {
        const r = await fetch(`${API}/lista-jogos`);
        const jogos = await r.json();
        tbody.innerHTML = jogos.map((j) => `
      <tr>
        <td>${j.id}</td>
        <td>${j.titulo}</td>
        <td>${j.genero}</td>
        <td>${j.plataforma}</td>
        <td>${j.desenvolvedora}</td>
        <td>${j.anoLancamento}</td>
        <td>R$ ${j.preco.toFixed(2)}</td>
        <td>${j.estoque}</td>
        <td>
          <button class="btn btn-danger btn-sm" onclick="remover(${j.id},'${j.titulo}')">✕</button>
        </td>
      </tr>`).join("");
    }
    catch (_a) {
        tbody.innerHTML = '<tr><td colspan="9" class="text-danger text-center">Erro ao conectar à API.</td></tr>';
    }
}
async function cadastrarJogo() {
    const titulo = document.getElementById("titulo").value.trim();
    const genero = document.getElementById("genero").value;
    const plataforma = document.getElementById("plataforma").value;
    const preco = parseFloat(document.getElementById("preco").value);
    const estoque = parseInt(document.getElementById("estoque").value);
    const desenvolvedora = document.getElementById("desenvolvedora").value.trim();
    const anoLancamento = parseInt(document.getElementById("ano").value);
    if (!titulo || !genero || !plataforma || !desenvolvedora || isNaN(preco) || isNaN(estoque) || isNaN(anoLancamento)) {
        mostrarAlerta("Preencha todos os campos.", "warning");
        return;
    }
    const dto = { titulo, genero, plataforma, preco, estoque, desenvolvedora, anoLancamento };
    const btn = document.querySelector("button.btn-success");
    const spinner = document.getElementById("spinner");
    btn.disabled = true;
    spinner.classList.remove("d-none");
    try {
        const r = await fetch(`${API}/cadastrar-jogo`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(dto)
        });
        if (r.ok) {
            const jogo = await r.json();
            mostrarAlerta(`Jogo "${jogo.titulo}" cadastrado com sucesso!`, "success");
            limpar();
            carregarJogos();
        }
        else {
            const erro = await r.json();
            mostrarAlerta(erro.erros ? erro.erros.join(" | ") : "Erro ao cadastrar.", "danger");
        }
    }
    catch (_a) {
        mostrarAlerta("Não foi possível conectar à API.", "danger");
    }
    finally {
        btn.disabled = false;
        spinner.classList.add("d-none");
    }
}
async function remover(id, titulo) {
    if (!confirm(`Remover "${titulo}"?`))
        return;
    const r = await fetch(`${API}/remover-jogo/${id}`, { method: "DELETE" });
    if (r.ok) {
        mostrarAlerta(`"${titulo}" removido.`, "success");
        carregarJogos();
    }
}
function limpar() {
    ["titulo", "desenvolvedora", "preco", "estoque", "ano"].forEach(id => document.getElementById(id).value = "");
    document.getElementById("genero").value = "";
    document.getElementById("plataforma").value = "";
}
document.addEventListener("DOMContentLoaded", carregarJogos);
