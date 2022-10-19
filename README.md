## Cinemark

### Descrição da aplicação realizada

A aplicação foi desenvolvida no .Net Core 6 utilizando o Clean Architecture para estrutura do projeto.

O projeto também contem a utilização dos seguintes recursos:

- Banco de dados MongoDB
- Cache com Redis
- RabbitMQ para mensagens
- Autenticação com JWT
- Swagger

Para utilização da aplicação local existe no projeto o arquivo `docker-compose.yml` que poderá ser utilizado para criação dos container para funcionamento da aplicação.

### Como usar a autenticação

Foi criado um cadastro de usuário para conseguir utilizar a autenticação.

Então ao utlizar os endpoints de filmes deverá criar um usuário e utilizar a rota de login para criação do token para autenticação.

A validação foi apenas a título de exemplo, para validar que a aplicação responde apenas com o autenticação.

### Observações sobre o código do projeto

- Foram criados alguns testes de unidade, não foi criado da aplicação toda.
- O cache está sendo invalidado a todo filme que é criado, alterado ou excluído.
- O consumidor das mensagens após o CRUD do filme está como título de exemplo, mas a ideia seria ir atualizando a Query para consulta dos filmes.
