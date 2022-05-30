CREATE TABLE [dbo].[TBQuestao] (
    [Numero]         INT          IDENTITY (1, 1) NOT NULL,
    [Bimestre]       INT          NULL,
    [Materia_Numero] INT          NULL,
    [Titulo]         VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBQuestao_ToTable] FOREIGN KEY ([Materia_Numero]) REFERENCES [dbo].[TBMateria] ([Numero])
);

