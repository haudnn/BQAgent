﻿using Microsoft.Extensions.VectorData;

namespace Agent.Chat.Services;

public class SemanticSearchRecord
{
  [VectorStoreRecordKey]
  public required Guid Key { get; set; }

  [VectorStoreRecordData(IsFilterable = true)]
  public required string FileName { get; set; }

  [VectorStoreRecordData]
  public int PageNumber { get; set; }

  [VectorStoreRecordData]
  public required string Text { get; set; }

  [VectorStoreRecordVector(1536, DistanceFunction.CosineSimilarity)] // 1536 is the default vector size for the OpenAI text-embedding-3-small model
  public ReadOnlyMemory<float> Vector { get; set; }
}
