public static class LookUpData
{

    // the maximum amount of items the inventory can hold in a single slot 
    public static readonly int maxNumberOfItemsPerSlot = 10;

    // the height of all chunks
    public static readonly int chunkHeight = 128;

    // the width of all chunks
    public static readonly int chunkWidth = 16;

    // the number of chunks on each side loaded around the player  
    public static readonly int renderDisctance = 2;

    // the range of the mouse from the player
    public static readonly float playerMouseRange = 3f;

    // noise scale for biome generation
    public static readonly float biomeGenerationScale = 0.9f;

    // threshold for cave generation
    public static readonly float caveGenerationThreshold = 0.4f;

    // scale of the cave generation
    public static readonly float caveScale = 0.2f;
}
