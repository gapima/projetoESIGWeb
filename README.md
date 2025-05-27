## 📑 Sobre o Projeto

Este projeto foi desenvolvido como parte do processo seletivo para Pessoa Desenvolvedora Web .NET na ESIG Group. A aplicação foi construída em **ASP.NET Web Forms**, conectando-se a um banco Oracle 11g+, e implementa todos os requisitos obrigatórios da atividade, além de diversos diferenciais para proporcionar uma experiência mais robusta e profissional.

---

## ✔️ Funcionalidades Implementadas (Cronologia da Atividade)

1. **Criação do Banco de Dados Oracle**

   * Desenvolvi scripts SQL para criação das 6 tabelas principais (cargo, vencimentos, pessoa, cargo_vencimentos, pessoa_salario, usuario), respeitando a ordem das constraints, além da procedure SP_CALCULAR_SALARIOS e da view VW_RELATORIO_SALARIOS.
    **Estao na pasta /script oracle/ Scrips ja tem os inserts para popular a tabela com dados do excel enviado.**
     
2. **Tela de Login e Cadastro de Usuário**

   * Implementei telas para login seguro e cadastro de novos usuários no sistema, garantindo o acesso restrito às funcionalidades.

3. **Tela de Listagem de Pessoas e Salários**

   * Criei uma tela central de listagem de pessoas, exibindo nome, cargo, salário bruto, descontos e salário líquido, conforme solicitado.
   * Para facilitar o uso com grandes volumes de dados (mais de 3 mil registros), desenvolvi paginação eficiente e filtros por nome e cargo.

4. **Cálculo de Salários via Procedure Oracle**

   * Implementei a integração para executar a procedure `SP_CALCULAR_SALARIOS` de forma assíncrona, automatizando o preenchimento da tabela `pessoa_salario` após qualquer alteração relevante (como edição de créditos/débitos).
   - **Todo o processamento do sistema foi projetado utilizando métodos assíncronos (`async`/`await`), incluindo todas as operações de CRUD, consultas, chamadas de procedures e carregamento de dados, proporcionando maior desempenho e melhor experiência do usuário, especialmente com grandes volumes de dados.**

5. **Vinculação de Créditos/Débitos**

   * Desenvolvi uma interface modal onde é possível vincular créditos e débitos a cargos, além de criar ou editar os vencimentos disponíveis no sistema. Também adicionei uma submodal para cadastro rápido de novos créditos/débitos.

6. **CRUD de Pessoas**

   * Implementei a criação, edição e exclusão de pessoas com modais detalhados, facilitando o cadastro e a manutenção das informações pessoais e de vínculo ao cargo.

7. **Relatório de Salários (Crystal Reports)**

   * Desenvolvi um relatório Crystal Reports para exibição detalhada dos salários calculados, utilizando a view criada para facilitar a consulta dos dados.

---

## 💎 Funcionalidades Extras & Melhorias

* **Paginação e Filtros Avançados**
  Otimizei a experiência de navegação para grandes bases de dados, com filtros instantâneos por cargo e nome.

* **Validações de Formulários (UX)**
  Todas as telas de cadastro e edição possuem validação visual de obrigatoriedade, com feedback claro ao usuário.

* **Estruturação Profissional**
  O projeto foi organizado em múltiplas camadas:

  * Models, Repository, Service, Controls, Pages, Reports
    Facilitando manutenção, testes e extensibilidade.

* **Design Responsivo e Moderno**
  Utilizei Bootstrap para garantir uma interface responsiva e agradável.

* **Validação de CEP por API**  
  Para validar automaticamente o CEP informado no cadastro de pessoas, utilizei a [BrasilAPI - Consulta CEP](https://brasilapi.com.br/api/cep/v1/08543070).


---

## 🛠️ Como Rodar o Projeto Localmente

### 1. Pré-requisitos

* Visual Studio 2019 ou superior
* Oracle Database 11g ou superior
* Oracle ManagedDataAccess para .NET
* Crystal Reports Runtime (caso não tenha)

### 2. Clone o Repositório

bash
git clone [git@github.com:gapima/projetoESIGWeb.git]



### 3. Crie o Banco de Dados

* Execute o arquivo scripts.sql na raiz do projeto no Oracle SQL Developer, **respeitando a ordem das tabelas**.
* O script cria as tabelas, a procedure e a view necessárias.

### 4. Ajuste a Connection String

No Web.config:

xml
<connectionStrings>
  <add name="OracleConnection" connectionString="User Id=SEU_USUARIO; Password=SUA_SENHA; Data Source=localhost:1521/xe;" providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>



### 5. Execute o Projeto

* Abra a solução no Visual Studio e pressione F5.

---

## 📷 Exemplos de Telas

### Listagem de Pessoas e Salários
![Listagem](/prints/listagem.png)

### Modal Vincular Créditos/Débitos
![Vincular Créditos](/prints/vincVenc.png)

### Modal Nova Pessoa
![Nova Pessoa](/prints/novapessoa.png)

### Detalhes Financeiros da Pessoa
![Detalhes Financeiros](/prints/detalheFinPessoa.png)

### Relatório Crystal Reports
![Relatório](/prints/relatorio.png)

### Tela de Login
![Login](/prints/login.png)

### Cadastro de Usuário
![Cadastro Usuário](/prints/loginCriar.png)

---

## 🔒 Informações de Login

* Para testar, cadastre um novo usuário na tela de cadastro de usuário.

---

## 📝 Observações

* Caso tenha dificuldades com o Crystal Reports, confira se o runtime está instalado.
* O projeto foi testado com Oracle XE 11g e Visual Studio 2022.

---

## 📬 Contato

Gabriel Lima – \[[gapima7@gmial.com](mailto:gapima7@gmial.com)]

---

**Agradeço a oportunidade de participar do processo seletivo!**
