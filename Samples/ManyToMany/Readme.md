# ManyToMany


By default - Sql
- PublicationOeuvres
  - PublicationOeuvreId
- Notes
  - NoteId
- NotePublicationOeuvre
  - NotesNoteId
  - PublicationOeuvresPublicationOeuvreId


Structure
``` sql
select * from PublicationOeuvre
select * from PublicationOeuvreNote
select * from Note

CREATE TABLE [dbo].[PublicationOeuvre] (
    [PublicationOeuvreId] INT IDENTITY (1, 1) NOT NULL,
    [Titre] NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_PublicationOeuvre] PRIMARY KEY CLUSTERED ([PublicationOeuvreId] ASC),
);
GO

CREATE TABLE [dbo].[Note]
(
    [NoteId] INT IDENTITY(1,1) NOT NULL,
    [Contenu] NVARCHAR(4000) NOT NULL,
    [DateCreation] DATETIMEOFFSET(3) NOT NULL,
    [UtilisateurCreationId] INT NOT NULL,
    CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED ([NoteId] ASC)
)
GO
CREATE UNIQUE INDEX [IX_Note_NoteId] ON [dbo].[Note] ([NoteId])

ALTER TABLE [dbo].[Note]  WITH CHECK ADD  CONSTRAINT [FK_Note_UtilisateurCreation] FOREIGN KEY([UtilisateurCreationId])
REFERENCES [dbo].[Utilisateur] ([UtilisateurId])

CREATE TABLE [dbo].[PublicationOeuvreNote]
(
    [PublicationOeuvreId] INT NOT NULL,
    [NoteId] INT NOT NULL,
    CONSTRAINT [PK_PublicationOeuvreNote] PRIMARY KEY CLUSTERED ( [PublicationOeuvreId] ASC, [NoteId] ASC ),
    CONSTRAINT [FK_PublicationOeuvreNote_Note] FOREIGN KEY([NoteId]) REFERENCES [dbo].[Note] ([NoteId]),
    CONSTRAINT [FK_PublicationOeuvreNote_PublicationOeuvre] FOREIGN KEY([PublicationOeuvreId]) REFERENCES [dbo].[PublicationOeuvre] ([PublicationOeuvreId])
)
GO

CREATE TABLE [dbo].[Utilisateur](
    [UtilisateurId] [int] NOT NULL,
    [Nom] [nvarchar](60) NOT NULL,
    [Prenom] [nvarchar](60) NOT NULL,
    CONSTRAINT [PK_Utilisateur] PRIMARY KEY CLUSTERED
)
GO
```



## Resources

- https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many#understanding-many-to-many-relationships
