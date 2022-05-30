CREATE TABLE [dbo].[TBMateria] (
    [Numero]       INT          IDENTITY (1, 1) NOT NULL,
    [Titulo]       VARCHAR (50) NULL,
    [Serie]        INT          NULL,
    [Discplina_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBMateria_ToTable] FOREIGN KEY ([Discplina_id]) REFERENCES [dbo].[TBDisciplina] ([Id])
);

