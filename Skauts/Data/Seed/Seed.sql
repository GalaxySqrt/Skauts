/*
================================================================
SCRIPT DE SEED (POVOAMENTO) - BANCO DE DADOS SKAUTS (CORRIGIDO E MODIFICADO)
================================================================
*/

-- 1. Inserir Tabelas de Lookup (sem dependências)
PRINT 'Inserindo dados em roles, event_types, prize_types...';

INSERT INTO roles (acronym, name)
VALUES
('GOL', 'Goleiro'),
('ZAG', 'Zagueiro'),
('LAT', 'Lateral'),
('MCO', 'Meio Campo'),
('ATA', 'Atacante');

INSERT INTO event_types (name)
VALUES
('Gol'),
('Assistência'),
('Cartão Amarelo'),
('Cartão Vermelho'),
('Substituição (Entra)'),
('Substituição (Sai)');

INSERT INTO prize_types (name)
VALUES
('Melhor em Campo'),
('Artilheiro do Campeonato'),
('Melhor Goleiro'),
('Revelação');

-- 2. Inserir Entidades Principais (Organização e Usuário Admin)
PRINT 'Inserindo organizations e users...';

-- Org 1
INSERT INTO organizations (name, created_at)
VALUES ('Liga Skauts de Exemplo', GETDATE());
DECLARE @OrgId INT = SCOPE_IDENTITY();

-- Org 2
INSERT INTO organizations (name, created_at)
VALUES ('AFR - Associação de Futebol Real', GETDATE());
DECLARE @OrgIdAFR INT = SCOPE_IDENTITY();

-- User 1
INSERT INTO users (email, password_hash, created_at)
VALUES ('admin@skauts.com', '$2a$12$4ENurxk6PRgqCYepn.smuu8yMf60FX/CYdnVI38pX4cpId7qyzR82', GETDATE());
DECLARE @UserId INT = SCOPE_IDENTITY();

-- User 2
INSERT INTO users (email, password_hash, created_at)
VALUES ('admin-afr@skauts.com', '$2a$12$H.GvS.y6wpl/y/3v/R0Dfeq5wTgo7xL/81N.Lh/M0/Q.f.M2/R.eO', GETDATE());
DECLARE @UserIdAFR INT = SCOPE_IDENTITY();

-- 3. Vincular Usuário e Organização (Tabela N:N)
PRINT 'Vinculando usuários às organizações...'; -- MODIFICADO --

-- Admin original (admin@skauts.com) -> Org 1 (Skauts Exemplo)
INSERT INTO users_organizations (user_id, org_id, admin)
VALUES (@UserId, @OrgId, 1);

-- Admin original (admin@skauts.com) -> Org 2 (AFR)
INSERT INTO users_organizations (user_id, org_id, admin)
VALUES (@UserId, @OrgIdAFR, 1);

-- Novo Admin (admin-afr@skauts.com) -> Org 2 (AFR)
INSERT INTO users_organizations (user_id, org_id, admin)
VALUES (@UserIdAFR, @OrgIdAFR, 1);

-- 4. Criar Entidades Dependentes (Times e Jogadores)
PRINT 'Inserindo teams e players...';

DECLARE @RoleIdGoleiro INT, @RoleIdZagueiro INT, @RoleIdAtacante INT, @RoleIdMeia INT;
SELECT @RoleIdGoleiro = id FROM roles WHERE acronym = 'GOL';
SELECT @RoleIdZagueiro = id FROM roles WHERE acronym = 'ZAG';
SELECT @RoleIdMeia = id FROM roles WHERE acronym = 'MCO';
SELECT @RoleIdAtacante = id FROM roles WHERE acronym = 'ATA';

-- ERRO 2: Variáveis de ID de Jogador agora são únicas
DECLARE @PlayerMeia1Id INT, @PlayerMeia2Id INT, @PlayerMessiId INT, @PlayerCr7Id INT, @PlayerNeymarId INT;
DECLARE @PlayerGoleiroId INT, @PlayerAtacanteId INT, @PlayerMeia3Id INT;

-- Jogadores da Org 1
INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Luiz "Luizito" Martins', @OrgId, @RoleIdMeia, 8, 8, 'luizito@skauts.com', '2000-04-30', GETDATE());
SET @PlayerMeia1Id = SCOPE_IDENTITY();

INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Caio "Caioba" Vilquer', @OrgId, @RoleIdMeia, 6, 7, 'caiovilquer@skauts.com', '2002-06-08', GETDATE());
SET @PlayerMeia2Id = SCOPE_IDENTITY();

INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Lionel "Messi" Messi', @OrgId, @RoleIdAtacante, 10, 10, 'messi@skauts.com', '1987-06-24', GETDATE());
SET @PlayerMessiId = SCOPE_IDENTITY();

INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Cristiano "CR7" Ronaldo', @OrgId, @RoleIdAtacante, 10, 10, 'cr7@skauts.com', '1985-02-05', GETDATE());
SET @PlayerCr7Id = SCOPE_IDENTITY();

INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Neymar "Ney" Junior', @OrgId, @RoleIdAtacante, 10, 10, 'neymar@skauts.com', '1992-02-05', GETDATE());
SET @PlayerNeymarId = SCOPE_IDENTITY();

INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Bruno "Muralha"', @OrgId, @RoleIdGoleiro, 8, 7, 'bruno@skauts.com', '1995-03-10', GETDATE());
SET @PlayerGoleiroId = SCOPE_IDENTITY();

INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Carlos "El Matador" Silva', @OrgId, @RoleIdAtacante, 9, 6, 'carlos@skauts.com', '1999-11-20', GETDATE());
SET @PlayerAtacanteId = SCOPE_IDENTITY();

INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Leo "Maestro" Costa', @OrgId, @RoleIdMeia, 7, 8, 'leo@skauts.com', '2001-01-15', GETDATE());
SET @PlayerMeia3Id = SCOPE_IDENTITY();

-- Jogadores para a AFR
INSERT INTO players (name, org_id, role_id, skill, physique, email, birth_date, created_at)
VALUES ('Luiz "LED" E#duardo', @OrgIdAFR, @RoleIdZagueiro, 8, 9, 'led@skauts.com', '1998-07-22', GETDATE());

-- Criar Times (na Org 1)
DECLARE @TimeAId UNIQUEIDENTIFIER = NEWID();
DECLARE @TimeBId UNIQUEIDENTIFIER = NEWID();

INSERT INTO teams (id, org_id, name, created_at)
VALUES (@TimeAId, @OrgId, 'Dragões Vermelhos FC', GETDATE());

INSERT INTO teams (id, org_id, name, created_at)
VALUES (@TimeBId, @OrgId, 'Tigres Azuis SC', GETDATE());

-- Vincular Jogadores aos Times (da Org 1)
INSERT INTO team_players (team_id, player_id, join_date)
VALUES
(@TimeAId, @PlayerGoleiroId, GETDATE()),
(@TimeAId, @PlayerAtacanteId, GETDATE()),
(@TimeBId, @PlayerMeia3Id, GETDATE()),
(@TimeAId, @PlayerMessiId, GETDATE()),
(@TimeBId, @PlayerCr7Id, GETDATE()),
(@TimeBId, @PlayerNeymarId, GETDATE());

-- 5. Criar Campeonato e Partida (na Org 1)
PRINT 'Inserindo championships e matches...';

INSERT INTO championships (org_id, name, start_date, end_date)
VALUES (@OrgId, 'Torneio de Abertura 2025', '2025-11-15', '2025-12-20');
DECLARE @ChampionshipId INT = SCOPE_IDENTITY();

-- Criar a Partida
INSERT INTO matches (org_id, team_a_id, team_b_id, [date], championship_id)
VALUES (@OrgId, @TimeAId, @TimeBId, '2025-11-20T16:00:00', @ChampionshipId);
DECLARE @MatchId INT = SCOPE_IDENTITY();

-- 6. Criar Eventos e Prêmios da Partida (da Org 1)
PRINT 'Inserindo events e players_prizes...';

DECLARE @EventTypeIdGol INT, @EventTypeIdAmarelo INT;
SELECT @EventTypeIdGol = id FROM event_types WHERE name = 'Gol';
SELECT @EventTypeIdAmarelo = id FROM event_types WHERE name = 'Cartão Amarelo';

-- Inserir Eventos da Partida
INSERT INTO events (match_id, player_id, event_type_id, event_time)
VALUES
(@MatchId, @PlayerAtacanteId, @EventTypeIdGol, '2025-11-20T16:25:00'),
(@MatchId, @PlayerMessiId, @EventTypeIdGol, '2025-11-20T17:10:00'),
(@MatchId, @PlayerCr7Id, @EventTypeIdGol, '2025-11-20T17:30:00'),
(@MatchId, @PlayerMeia3Id, @EventTypeIdAmarelo, '2025-11-20T16:45:00');

-- Capturar ID do Tipo de Prêmio
DECLARE @PrizeIdMvp INT;
SELECT @PrizeIdMvp = id FROM prize_types WHERE name = 'Melhor em Campo';

-- Dar Prêmio ao Messi
INSERT INTO players_prizes (player_id, prize_type_id, receive_date)
VALUES (@PlayerMessiId, @PrizeIdMvp, '2025-11-20');

PRINT '================================================';
PRINT 'Script de Seed concluído com sucesso!';
PRINT 'Usuário admin@skauts.com (senha: admin123) criado e vinculado a 2 orgs.';
PRINT 'Usuário admin-afr@skauts.com (senha: real123) criado e vinculado à AFR.';
PRINT '================================================';