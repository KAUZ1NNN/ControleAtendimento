📞 Controle de Atendimentos

Sistema para gerenciamento de chamados de suporte, permitindo acompanhar todo o fluxo de atendimento desde a abertura até a finalização, com controle de tempo e identificação de atrasos.

🚀 Tecnologias
C# com ASP.NET Core (.NET 8)
PostgreSQL
HTML, CSS e JavaScript
⚙️ Funcionalidades
Cadastro de Setores
Cadastro de Prioridades com tempo estimado
Abertura de chamados com status inicial Aberto
Início de atendimento (check-in)
Finalização de atendimento (check-out) com registro de solução
Cálculo automático do tempo de atendimento
Listagem completa de chamados com:
Setor
Prioridade
Status
Tempo de atendimento
Destaque visual para chamados em atraso
🧠 Regras de Negócio
Não é possível iniciar um chamado já finalizado
Não é possível finalizar um chamado que não foi iniciado
O tempo de atendimento é calculado automaticamente
Chamados que ultrapassam o tempo da prioridade são marcados como atrasados
🛠️ Como Executar
🔹 Banco de Dados
Criar banco no PostgreSQL
Executar o script:
/database/script.sql
🔹 Backend
dotnet run

A API estará disponível em:

http://localhost:5202

Swagger:

http://localhost:5202/swagger
🔹 Frontend
cd frontend
npx serve

Acesse:

http://localhost:3000
📌 Estrutura do Projeto
ControleAtendimentos/
├── Controllers/
├── Models/
├── database/
├── frontend/
├── Program.cs
└── README.md
💡 Diferenciais
Aplicação de regras de negócio no backend
Interface simples para interação com o sistema
Uso de selects para melhorar a usabilidade
Identificação automática de chamados em atraso
👨‍💻 Autor

Kauan Casemiro
