# language: pt-BR

Funcionalidade: Gestão de Acidentes
  Para monitorar incidentes de trânsito
  Como usuário da API
  Quero ver a lista de acidentes e detalhes de um acidente

  Cenário: Listar todos os acidentes
    Dado que a API está disponível em "http://localhost:5215"
    Quando faço GET em "/acidentes"
    Então o status code deve ser 200
    E a resposta deve ser um array JSON não vazio
    E a resposta deve obedecer ao schema "acidentes-schema.json"

  Cenário: Buscar acidente inexistente
    Dado que a API está disponível em "http://localhost:5215"
    Quando faço GET em "/acidentes/9999"
    Então o status code deve ser 404
