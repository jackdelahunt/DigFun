using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LodeManager
{
   public static Lode[] globalLodes = new Lode[] {
       new Lode("Dirt", 1, 0, LookUpData.chunkHeight, 0.3f, 0.7f, 2342)
   };
}
