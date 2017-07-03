using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class LevelMetadata
    {
        public List<LevelMetaResource> Levels = new List<LevelMetaResource>();
    }

    [Serializable]
    public class LevelMetaResource
    {
        public int OrdinalNumber { get; set; }
        public string LevelName { get; set; }
        public string ResourceName { get; set; }

    }
}