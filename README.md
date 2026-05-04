# 📞 Controle de Atendimentos

Projeto desenvolvido como desafio técnico para vaga de estágio.

Sistema para gerenciar chamados de suporte do início ao fim, com controle de tempo e identificação de atrasos.

---

## 🚀 Tecnologias

* C# (.NET 8)
* PostgreSQL
* HTML, CSS e JavaScript

---

## ⚙️ Funcionalidades

* Cadastro de setores
* Cadastro de prioridades com tempo estimado
* Abertura de chamados (status inicial: Aberto)
* Início de atendimento
* Finalização com registro de solução
* Cálculo automático do tempo de atendimento
* Listagem completa dos chamados
* Destaque para chamados em atraso

---

## 🧠 Regras de Negócio

* Não é possível iniciar um chamado já finalizado
* Não é possível finalizar sem iniciar antes
* O tempo de atendimento é calculado automaticamente
* Chamados que passam do tempo da prioridade são marcados como atrasados

---

## 🛠️ Como rodar o projeto

### Banco de Dados

Criar um banco no PostgreSQL e executar:

```id="h3m5zj"
/database/script.sql
```

---

### Backend

```id="m8q1ty"
dotnet run
```

A API ficará disponível em:

```id="d0n9wr"
http://localhost:5202
```

---

### Frontend

```id="l6x2bz"
cd frontend
npx serve
```

Acesse:

```id="f1q7ke"
http://localhost:3000
```

---

## 💡 Observações

O sistema aplica regras de negócio no backend para garantir consistência dos dados, além de calcular automaticamente o tempo de atendimento e identificar atrasos.

---

## 👨‍💻 Autor

Kauan Casemiro
