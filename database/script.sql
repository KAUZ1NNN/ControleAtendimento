CREATE TABLE setores (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100)
);

CREATE TABLE prioridades (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(50),
    tempo_estimado_horas INT
);

CREATE TABLE chamados (
    id SERIAL PRIMARY KEY,
    titulo VARCHAR(200),
    descricao TEXT,
    setor_id INT REFERENCES setores(id),
    prioridade_id INT REFERENCES prioridades(id),
    status VARCHAR(50),
    data_criacao TIMESTAMP
);

CREATE TABLE atendimentos (
    id SERIAL PRIMARY KEY,
    chamado_id INT REFERENCES chamados(id),
    data_inicio TIMESTAMP,
    data_fim TIMESTAMP,
    solucao TEXT
);

-- dados iniciais
INSERT INTO setores (nome) VALUES ('TI'), ('RH'), ('Suporte'), ('Gerencia');

INSERT INTO prioridades (nome, tempo_estimado_horas) VALUES
('Baixa', 72),
('Média', 24),
('Alta', 4);