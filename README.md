<div align="center">

# 🎮 Loja de Jogos

**Sistema de gerenciamento de catálogo de jogos digitais e físicos**

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

</div>

---

## 📖 Sobre o Projeto

A **Loja de Jogos** é uma Web API REST desenvolvida com **ASP.NET Core**, que permite gerenciar um catálogo de jogos digitais e físicos. O sistema oferece operações de cadastro, listagem, busca e remoção de jogos, com validações de regras de negócio e uma interface web integrada.

> Projeto desenvolvido como trabalho prático da disciplina, aplicando conceitos de **arquitetura REST**, **assincronismo**, **injeção de dependência** e consumo de API via **TypeScript**.

---

## ✨ Funcionalidades

- ✅ Cadastrar jogos com validações de regras de negócio
- ✅ Listar todos os jogos cadastrados
- ✅ Buscar jogo por ID
- ✅ Remover jogos do catálogo
- ✅ Interface web com formulário e tabela dinâmica
- ✅ Documentação automática via Swagger

---

## 🏗️ Estrutura do Projeto

```
LojaDeJogos/
├── 📁 Controllers/
│   └── JogosController.cs       # Endpoints da API
├── 📁 DTOs/
│   └── JogoDto.cs               # Objeto de entrada do POST
├── 📁 Interfaces/
│   └── IValidadorJogo.cs        # Contrato da injeção de dependência
├── 📁 Models/
│   └── Jogo.cs                  # Entidade principal
├── 📁 Services/
│   └── ValidadorJogo.cs         # Implementação das regras de negócio
├── 📁 wwwroot/
│   ├── index.html               # Interface web
│   ├── app.ts                   # Lógica TypeScript
│   └── app.js                   # TypeScript compilado
└── Program.cs                   # Configuração geral da aplicação
```

---

## 🔗 Endpoints da API

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/api/loja-de-jogos/lista-jogos` | Lista todos os jogos |
| `GET` | `/api/loja-de-jogos/buscar-jogo/{id}` | Busca um jogo por ID |
| `POST` | `/api/loja-de-jogos/cadastrar-jogo` | Cadastra um novo jogo |
| `DELETE` | `/api/loja-de-jogos/remover-jogo/{id}` | Remove um jogo |

---

## 🧠 Conceitos Aplicados

### 🔄 Assincronismo
O método de cadastro utiliza `async Task<IActionResult>` com `await Task.Delay(2000)` simulando a persistência em banco de dados, demonstrando o conceito de não bloqueio de threads.

### 💉 Injeção de Dependência
- **Interface** `IValidadorJogo` define o contrato do serviço
- **Classe** `ValidadorJogo` implementa 5 regras de negócio reais
- **Controller** recebe a interface pelo construtor (desacoplamento)
- Registrado como **Scoped** no `Program.cs`

### ✔️ Regras de Negócio
1. Título não pode ser composto apenas por números
2. Gênero deve ser um valor válido da lista permitida
3. Plataforma deve ser um valor válido da lista permitida
4. Jogos com estoque devem ter preço maior que zero
5. Ano de lançamento não pode ultrapassar o ano seguinte

---

## 🖥️ Modelo de Dados

```json
{
  "id": 1,
  "titulo": "The Witcher 3",
  "genero": "RPG",
  "plataforma": "PC",
  "preco": 99.90,
  "estoque": 10,
  "desenvolvedora": "CD Projekt Red",
  "anoLancamento": 2015,
  "cadastradoEm": "2025-01-01T00:00:00"
}
```

---

## 🚀 Como Rodar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou VS Code

### Passo a passo

```bash
# Clone o repositório
git clone https://github.com/Lucio-franca/LojaDeJogos.git

# Acesse a pasta do projeto
cd LojaDeJogos

# Rode a aplicação
dotnet run
```

Acesse no navegador:

| Interface | URL |
|-----------|-----|
| 🌐 Front-end | `http://localhost:5000` |
| 📋 Swagger | `http://localhost:5000/swagger` |

---

## 📌 Observação

Os jogos ficam salvos **em memória** enquanto a API está rodando. Ao reiniciar, os 3 jogos iniciais são restaurados. Isso é intencional — o `Task.Delay` simula o banco de dados sem usar um real.

---

## 🛠️ Tecnologias

- **ASP.NET Core 8** — Web API REST
- **TypeScript** — Lógica do front-end
- **Bootstrap 5** — Estilização da interface
- **Swagger / Swashbuckle** — Documentação da API
- **C# DataAnnotations** — Validações do DTO

---

<div align="center">

Desenvolvido por [Lucio França](https://github.com/Lucio-franca) 🎮

</div>
