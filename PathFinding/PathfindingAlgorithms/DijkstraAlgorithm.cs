using System.Collections.Generic;

public class DijkstraAlgorithm : IPathfindingAlgorithm
{
    public List<INode> GetPath(List<INode> Nodes, INode start, INode end)
    {
        List<INode> notVisited = new List<INode>(Nodes);

        var track = new Dictionary<INode, DijkstraData>
        {
            [start] = new DijkstraData { Price = 0, Previous = null }
        };

        while (true)
        {
            INode toOpen = GetNodeWithLowestPrice(notVisited, track);

            if (toOpen == null)
                return new List<INode>();

            if (toOpen == end)
                break;

            UpdateTrackAndPrices(toOpen, track);

            notVisited.Remove(toOpen);
        }

        return GetShortestPath(end, track);
    }

    private INode GetNodeWithLowestPrice(List<INode> nodes, Dictionary<INode, DijkstraData> track)
    {
        INode toOpen = null;
        var bestPrice = double.PositiveInfinity;

        foreach (var node in nodes)
        {
            if (track.ContainsKey(node) && track[node].Price < bestPrice)
            {
                bestPrice = track[node].Price;
                toOpen = node;
            }
        }

        return toOpen;
    }

    private void UpdateTrackAndPrices(INode currentNode, Dictionary<INode, DijkstraData> track)
    {
        foreach (var edge in currentNode.Edges)
        {
            var currentPrice = track[currentNode].Price + edge.Distance;
            var nextNode = edge.OtherNode(currentNode);

            if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
            {
                track[nextNode] = new DijkstraData { Previous = currentNode, Price = currentPrice };
            }
        }
    }

    private List<INode> GetShortestPath(INode endNode, Dictionary<INode, DijkstraData> track)
    {
        var result = new List<INode>();

        while (endNode != null)
        {
            result.Insert(0,endNode);
            endNode = track[endNode].Previous;
        }
        
        return result;
    }
}

class DijkstraData
{
    public INode Previous { get; set; }
    public double Price { get; set; }
}