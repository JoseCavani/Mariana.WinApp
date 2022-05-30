CREATE TABLE [dbo].[TBAlternativas] (
    [Titulo]         VARCHAR (50) NOT NULL,
    [Correta]        BIT          NULL,
    [Questao_Numero] INT          NULL,
    PRIMARY KEY CLUSTERED ([Titulo] ASC),
    CONSTRAINT [FK_TBAlternativas_ToTable] FOREIGN KEY ([Questao_Numero]) REFERENCES [dbo].[TBQuestao] ([Numero]) ON DELETE CASCADE
);

