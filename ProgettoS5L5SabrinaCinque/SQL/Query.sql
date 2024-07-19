﻿--per il prof: ho riscritto le query come quelle della settimana scorsa,popolando le tabelle per avere già i record su cui fare i report



CREATE TABLE [dbo].[Tipo_Violazioni] (
  IdViolazione INT NOT NULL PRIMARY KEY IDENTITY,
  Descrizione NVARCHAR(255) NOT NULL,
  Importo DECIMAL(10,2) NOT NULL,--queste ultime due colonne le ho messe qui piuttosto che in verbali perch era poù logico
  DecurtamentoPunti INT NOT NULL
);


CREATE TABLE [dbo].[Anagrafiche] (
  IdAnagrafica INT NOT NULL PRIMARY KEY IDENTITY,
  Cognome NVARCHAR(30) NOT NULL,
  Nome NVARCHAR(30) NOT NULL,
  Indirizzo NVARCHAR(100) NOT NULL,
  Citta NVARCHAR(50) NOT NULL,
  Cap CHAR(5) NOT NULL,
  Cod_Fisc CHAR(16) NOT NULL,
  CONSTRAINT CK_Anagrafiche_Cod_Fisc CHECK (LEN(Cod_Fisc) = 16)
);

CREATE TABLE Verbali (
  IdVerbale INT NOT NULL PRIMARY KEY IDENTITY,
  DataViolazione DATE NOT NULL, --ho fatto solo DATE e non DATETIME2 perchè mi chiedeva anche l'orario, ma poi avevo difficoltà nelle query sotto 
  IndirizzoViolazione NVARCHAR(255) NOT NULL,
  Nominativo_Agente NVARCHAR(50) NOT NULL,
  DataTrascrizioneVerbale DATE NOT NULL,
  IdAnagrafica INT NOT NULL,
  IdViolazioneVerbale INT NOT NULL,
  CONSTRAINT FK_Verbali_Anagrafiche FOREIGN KEY (IdAnagrafica) REFERENCES Anagrafiche(IdAnagrafica),
  CONSTRAINT FK_Verbali_Tipo_Violazioni FOREIGN KEY (IdViolazioneVerbale) REFERENCES Tipo_Violazioni(IdViolazione)
);


--popoliamo le tabelle con record casuali
INSERT INTO [dbo].[Tipo_Violazioni] (Descrizione, Importo, DecurtamentoPunti)
VALUES 
('Eccesso di velocità', 150.00, 3),
('Sosta vietata', 50.00, 0),
('Guida senza cinture', 80.00, 2),
('Uso del cellulare', 100.00, 2),
('Passaggio con semaforo rosso', 200.00, 6),
('Mancata precedenza', 75.00, 1),
('Guida contromano', 120.00, 4),
('Mancato rispetto delle distanze', 60.00, 1),
('Guida in stato di ebbrezza', 300.00, 10),
('Assenza di assicurazione', 250.00, 5);



INSERT INTO [dbo].[Anagrafiche] (Cognome, Nome, Indirizzo, Citta, Cap, Cod_Fisc)
VALUES 
('Esposito', 'Mario', 'Via Roma, 1', 'Milano', '20100', 'RSSMRA80A01F205X'),
('Russo', 'Luca', 'Via Milano, 10', 'Torino', '10100', 'BNCGLC85A01L219K'),
('Bianchi', 'Giulia', 'Via Firenze, 20', 'Roma', '00100', 'VRDGLI90A01H501X'),
('Marrone', 'Sara', 'Via Napoli, 15', 'Napoli', '80100', 'NRISRA95A01E123X'),
('Romano', 'Fabio', 'Via Torino, 5', 'Torino', '10100', 'GLLFBA85A01L220L'),
('Barbieri', 'Elena', 'Via Venezia, 7', 'Venezia', '30100', 'RSALNE90A01H123C'),
('Ricci', 'Andrea', 'Via Genova, 12', 'Genova', '16100', 'BLUAND80A01G456Y'),
('Bianco', 'Francesco', 'Via Bari, 18', 'Bari', '70100', 'VLAFRC85A01L789D'),
('Ferrari', 'Chiara', 'Via Trieste, 2', 'Trieste', '34100', 'MRRCHA90A01H321Q'),
('Martini', 'Lorenzo', 'Via Perugia, 21', 'Perugia', '06100', 'AZZLRN85A01E654P'),
('Rubino', 'Simone', 'Via Trento, 11', 'Trento', '38100', 'BGESMN90A01H852B'),
('Attanasio', 'Alessia', 'Via Palermo, 13', 'Palermo', '90100', 'BNCALS85A01L951M'),
('Deluca', 'Giorgio', 'Via Cagliari, 14', 'Cagliari', '09100', 'ARCGGG90A01H741A'),
('Cinque', 'Martina', 'Via Sassari, 16', 'Sassari', '07100', 'FCXMRT85A01E963N'),
('Gialli', 'Marco', 'Via Reggio, 3', 'Reggio Emilia', '42100', 'OROMRC90A01H159T'),
('Mancini', 'Laura', 'Via Pisa, 6', 'Pisa', '56100', 'ARGHLR85A01L123J'),
('Romano', 'Gianni', 'Via Modena, 8', 'Modena', '41100', 'BRZGNN90A01E753G'),
('Rizzi', 'Eleonora', 'Via Parma, 10', 'Parma', '43100', 'ZFFELN85A01H852R'),
('Simeoni', 'Davide', 'Via Lecce, 17', 'Lecce', '73100', 'RBNDVD90A01L456S'),
('Tedeschi', 'Federica', 'Via Salerno, 9', 'Salerno', '84100', 'SMRFDC85A01E789L');


INSERT INTO Verbali (DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, IdAnagrafica, IdViolazioneVerbale)
VALUES 
('2023-01-10', 'Via Roma, 1', 'Agente Rossi', '2023-01-11', 1, 3),
('2023-01-15', 'Via Milano, 10', 'Agente Bianchi', '2023-01-16', 2, 1),
('2023-01-20', 'Via Firenze, 20', 'Agente Verdi', '2023-01-21', 3, 5),
('2023-01-25', 'Via Napoli, 15', 'Agente Neri', '2023-01-26', 4, 2),
('2023-01-30', 'Via Torino, 5', 'Agente Gialli', '2023-01-31', 5, 6),
('2023-02-05', 'Via Venezia, 7', 'Agente Rosa', '2023-02-06', 6, 4),
('2023-02-10', 'Via Genova, 12', 'Agente Blu', '2023-02-11', 7, 9),
('2023-02-15', 'Via Bari, 18', 'Agente Viola', '2023-02-16', 8, 7),
('2023-02-20', 'Via Trieste, 2', 'Agente Marrone', '2023-02-21', 9, 10),
('2023-02-25', 'Via Perugia, 21', 'Agente Azzurro', '2023-02-26', 10, 8),
('2023-03-01', 'Via Trento, 11', 'Agente Beige', '2023-03-02', 11, 3),
('2023-03-05', 'Via Palermo, 13', 'Agente Bianco', '2023-03-06', 12, 5),
('2023-03-10', 'Via Cagliari, 14', 'Agente Arancio', '2023-03-11', 13, 2),
('2023-03-15', 'Via Sassari, 16', 'Agente Fucsia', '2023-03-16', 14, 4),
('2023-03-20', 'Via Reggio, 3', 'Agente Oro', '2023-03-21', 15, 6),
('2023-03-25', 'Via Pisa, 6', 'Agente Argento', '2023-03-26', 16, 1),
('2023-03-30', 'Via Modena, 8', 'Agente Bronzo', '2023-03-31', 17, 7),
('2023-04-05', 'Via Parma, 10', 'Agente Zaffiro', '2023-04-06', 18, 9),
('2023-04-10', 'Via Lecce, 17', 'Agente Rubino', '2023-04-11', 19, 8),
('2023-04-15', 'Via Salerno, 9', 'Agente Smeraldo', '2023-04-16', 20, 10),
('2023-04-20', 'Via Roma, 1', 'Agente Rossi', '2023-04-21', 1, 2),
('2023-04-25', 'Via Milano, 10', 'Agente Bianchi', '2023-04-26', 2, 4),
('2023-04-30', 'Via Firenze, 20', 'Agente Verdi', '2023-05-01', 3, 1),
('2023-05-05', 'Via Napoli, 15', 'Agente Neri', '2023-05-06', 4, 5),
('2023-05-10', 'Via Torino, 5', 'Agente Gialli', '2023-05-11', 5, 6),
('2023-05-15', 'Via Venezia, 7', 'Agente Rosa', '2023-05-16', 6, 3),
('2023-05-20', 'Via Genova, 12', 'Agente Blu', '2023-05-21', 7, 7),
('2023-05-25', 'Via Bari, 18', 'Agente Viola', '2023-05-26', 8, 9),
('2023-05-30', 'Via Trieste, 2', 'Agente Marrone', '2023-05-31', 9, 10);



USE PoliziaMunicipale;


SELECT a.Cognome, a.Nome, SUM(tv.DecurtamentoPunti) AS TotalePuntiDecurtati
FROM Verbali v
JOIN Anagrafiche a ON v.IdAnagrafica = a.IdAnagrafica
JOIN Tipo_Violazioni tv ON v.IdViolazioneVerbale = tv.IdViolazione
GROUP BY a.Cognome, a.Nome;

