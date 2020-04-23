using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pinwheel.Griffin.TextureTool
{
    [System.Serializable]
    public struct GColorMapGeneratorParams
    {
        [SerializeField]
        private GStylizedTerrain terrain;
        public GStylizedTerrain Terrain
        {
            get
            {
                return terrain;
            }
            set
            {
                terrain = value;
            }
        }
    }
}