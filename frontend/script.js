const API = "http://localhost:5202/api";

async function criarChamado() {
    const data = {
        titulo: document.getElementById("titulo").value,
        descricao: document.getElementById("descricao").value,
        setorId: parseInt(document.getElementById("setorId").value),
        prioridadeId: parseInt(document.getElementById("prioridadeId").value)
    };

    await fetch(`${API}/Chamados`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });

    alert("Chamado criado!");
    listarChamados();
}

async function listarChamados() {
    const res = await fetch(`${API}/Chamados`);
    const dados = await res.json();

    const tabela = document.getElementById("tabela");
    tabela.innerHTML = "";

    dados.forEach(c => {

        let acoes = "";

        if (c.status === "Aberto") {
            acoes = `<button onclick="iniciar(${c.id})">Iniciar</button>`;
        } else if (c.status === "EmAtendimento") {
            acoes = `<button onclick="finalizar(${c.id})">Finalizar</button>`;
        } else {
            acoes = "—";
        }

        const linha = document.createElement("tr");

        if (c.atrasado) {
            linha.classList.add("atrasado");
        }

        linha.innerHTML = `
            <td>${c.id}</td>
            <td>${c.titulo}</td>
            <td>${c.setor}</td>
            <td>${c.prioridade}</td>
            <td>${c.status}</td>
            <td>${(c.tempoAtendimentoHoras || 0).toFixed(2)}</td>
            <td>${c.atrasado ? "SIM" : "NÃO"}</td>
            <td>${c.solucao ?? "-"}</td>
            <td>${acoes}</td>
        `;

        tabela.appendChild(linha);
    });
}

async function iniciar(id) {
    await fetch(`${API}/Chamados/${id}/iniciar`, { method: "POST" });
    listarChamados();
}

async function finalizar(id) {
    const solucao = prompt("Digite a solução:");
    if (!solucao) return;

    await fetch(`${API}/Chamados/${id}/finalizar`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(solucao)
    });

    listarChamados();
}

async function carregarPrioridades() {
    const res = await fetch(`${API}/Prioridades`);
    const dados = await res.json();

    const select = document.getElementById("prioridadeId");

    select.innerHTML = "";

    dados.forEach(p => {
        const option = document.createElement("option");
        option.value = p.id;
        option.textContent = p.nome;
        select.appendChild(option);
        if (select.options.length > 0) return;
    });
}
async function carregarSetores() {
    const res = await fetch(`${API}/Setores`);
    const dados = await res.json();

    const select = document.getElementById("setorId");
    select.innerHTML = "";

    dados.forEach(s => {
        const option = document.createElement("option");
        option.value = s.id;
        option.textContent = s.nome;
        select.appendChild(option);
    });
}

window.onload = () => {
    listarChamados();
    carregarPrioridades();
    carregarSetores();
};