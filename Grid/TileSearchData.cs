namespace ArcaneRecursion
{
    public class TileSearchData
    {
        public int Distance;
        public Tile[] Neighbors { get; private set; }
        public Tile NextWithSamePriority { get; set; }
        public Tile PathFrom { get; set; }
        public int SearchHeuristic { get; set; }
        public int SearchPriority { get { return Distance + SearchHeuristic; } }

        private readonly Tile _self;

        public TileSearchData(Tile tile)
        {
            _self = tile;
            Neighbors = new Tile[6];
        }

        public Tile GetNeighbor(HexDirection direction)
        {
            return Neighbors[(int)direction];
        }

        public void AddNeighbor(HexDirection direction, Tile tile)
        {
            Neighbors[(int)direction] = tile;
            tile.SearchData.SetNeighbor(HexMetrics.Opposite(direction), _self);
        }

        public void SetNeighbor(HexDirection direction, Tile tile)
        {
            Neighbors[(int)direction] = tile;
        }
    }
}