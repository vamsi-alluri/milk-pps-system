-- Correction Batches
CREATE TABLE [dbo].[CorrectionBatches] (
    [CorrectionBatchID] INT           IDENTITY (1, 1) NOT NULL,
    [BatchID]           INT           NULL,
    [Reason]            TEXT          NULL,
    [CorrectionTime]    DATETIME      DEFAULT (getdate()) NULL,
    [Correction]        VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([CorrectionBatchID] ASC),
    FOREIGN KEY ([BatchID]) REFERENCES [dbo].[Batches] ([BatchID])
);

