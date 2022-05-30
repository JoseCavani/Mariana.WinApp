CREATE TABLE [dbo].[TBTeste] (
    [Numero]         INT          IDENTITY (1, 1) NOT NULL,
    [Data]           DATETIME     NOT NULL,
    [NumeroQuestoes] INT          NOT NULL,
    [Discplina_id]   INT          NOT NULL,
    [Materia_Numero] INT          NULL,
    [Titulo]         VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBTeste_ToTable] FOREIGN KEY ([Discplina_id]) REFERENCES [dbo].[TBDisciplina] ([Id]),
    CONSTRAINT [FK_TBTeste_ToTable_1] FOREIGN KEY ([Materia_Numero]) REFERENCES [dbo].[TBMateria] ([Numero])
);

