CREATE TABLE [dbo].[TBTesteQuestao] (
    [Numero_Questao] INT NOT NULL,
    [Numero_Teste]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Numero_Teste] ASC, [Numero_Questao] ASC),
    CONSTRAINT [FK_Table_ToTable] FOREIGN KEY ([Numero_Questao]) REFERENCES [dbo].[TBQuestao] ([Numero]),
    CONSTRAINT [FK_Table_ToTable_1] FOREIGN KEY ([Numero_Teste]) REFERENCES [dbo].[TBTeste] ([Numero]) ON DELETE CASCADE
);

