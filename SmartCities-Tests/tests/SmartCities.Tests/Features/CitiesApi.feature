# language: pt-BR
Funcionalidade: Gestão de Cidades

  Cenário: Listar todas as cidades (positiva)
    Dado que a API está disponível em "https://localhost:5000"
    Quando faço GET em "/cities"
    Então o status code deve ser 200
    E a resposta deve ser um array JSON não vazio
    E a resposta deve obedecer ao schema "cities-schema.json"

  Cenário: Buscar cidade inexistente (negativa)
    Dado que a API está disponível em "https://localhost:5000"
    Quando faço GET em "/cities/9999"
    Então o status code deve ser 404
