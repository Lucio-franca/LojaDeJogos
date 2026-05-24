// ── Tipagem do objeto retornado pela API ──
interface Jogo {
    id: number;
    titulo: string;
    genero: string;
    plataforma: string;
    preco: number;
    estoque: number;
    desenvolvedora: string;
    anoLancamento: number;
    cadastradoEm: string;
}

// ── DTO enviado no POST ──
interface JogoDto {
    titulo: string;
    genero: string;
    plataforma: string;
    preco: number;
    estoque: number;
    desenvolvedora: string;
    anoLancamento: number;
}

const API: string = "/api/loja-de-jogos";

function mostrarAlerta(msg: string, tipo: string): void {
    const el = document.getElementById("alerta")!;
    el.className = `alert alert-${tipo}`;
    el.textContent = msg;
    setTimeout(() => el.className = "alert d-none", 5000);
}

async function carregarJogos(): Promise<void> {
    const tbody = document.getElementById("tabela")!;
    tbody.innerHTML = '<tr><td colspan="9" class="text-center">Carregando...</td></tr>';
    try {
        const r = await fetch(`${API}/lista-jogos`);
        const jogos: Jogo[] = await r.json();
        tbody.innerHTML = jogos.map((j: Jogo) => `
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
    } catch {
        tbody.innerHTML = '<tr><td colspan="9" class="text-danger text-center">Erro ao conectar à API.</td></tr>';
    }
}

async function cadastrarJogo(): Promise<void> {
    const titulo = (document.getElementById("titulo") as HTMLInputElement).value.trim();
    const genero = (document.getElementById("genero") as HTMLSelectElement).value;
    const plataforma = (document.getElementById("plataforma") as HTMLSelectElement).value;
    const preco = parseFloat((document.getElementById("preco") as HTMLInputElement).value);
    const estoque = parseInt((document.getElementById("estoque") as HTMLInputElement).value);
    const desenvolvedora = (document.getElementById("desenvolvedora") as HTMLInputElement).value.trim();
    const anoLancamento = parseInt((document.getElementById("ano") as HTMLInputElement).value);

    if (!titulo || !genero || !plataforma || !desenvolvedora || isNaN(preco) || isNaN(estoque) || isNaN(anoLancamento)) {
        mostrarAlerta("Preencha todos os campos.", "warning");
        return;
    }

    const dto: JogoDto = { titulo, genero, plataforma, preco, estoque, desenvolvedora, anoLancamento };

    const btn = document.querySelector("button.btn-success") as HTMLButtonElement;
    const spinner = document.getElementById("spinner")!;
    btn.disabled = true;
    spinner.classList.remove("d-none");

    try {
        const r = await fetch(`${API}/cadastrar-jogo`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(dto)
        });

        if (r.ok) {
            const jogo: Jogo = await r.json();
            mostrarAlerta(`Jogo "${jogo.titulo}" cadastrado com sucesso!`, "success");
            limpar();
            carregarJogos();
        } else {
            const erro = await r.json();
            mostrarAlerta(erro.erros ? erro.erros.join(" | ") : "Erro ao cadastrar.", "danger");
        }
    } catch {
        mostrarAlerta("Não foi possível conectar à API.", "danger");
    } finally {
        btn.disabled = false;
        spinner.classList.add("d-none");
    }
}

async function remover(id: number, titulo: string): Promise<void> {
    if (!confirm(`Remover "${titulo}"?`)) return;
    const r = await fetch(`${API}/remover-jogo/${id}`, { method: "DELETE" });
    if (r.ok) { mostrarAlerta(`"${titulo}" removido.`, "success"); carregarJogos(); }
}

function limpar(): void {
    ["titulo", "desenvolvedora", "preco", "estoque", "ano"].forEach(id =>
        (document.getElementById(id) as HTMLInputElement).value = "");
    (document.getElementById("genero") as HTMLSelectElement).value = "";
    (document.getElementById("plataforma") as HTMLSelectElement).value = "";
}

document.addEventListener("DOMContentLoaded", carregarJogos);