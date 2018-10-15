﻿using System;
using System.Collections.Generic;
using System.Text;
using Drachenhorn.Map.Common;
using Drachenhorn.Map.Random;

namespace Drachenhorn.Map.BSPT
{
    public static class LeafGenerator
    {
        public static TileType[][] GenerateLeaf()
        {
            var leafs = new List<Leaf>(); // flat rectangle store to help pick a random one
            Leaf root = new Leaf(0, 0, 60, 120); //
            leafs.Add(root); //populate rectangle store with root area
            while (leafs.Count < 19)
            { // this will give us 10 leaf areas
                int splitIdx = Randomizer.Get(0, leafs.Count); // choose a random element
                Leaf toSplit = leafs[splitIdx];
                if (toSplit.Split())
                { //attempt to split
                    leafs.Add(toSplit.LeftChild);
                    leafs.Add(toSplit.RightChild);
                }

            }
            root.GenerateDungeon(); //generate dungeons

            return PrintDungeons(leafs);
        }



        private static TileType[][] PrintDungeons(IList<Leaf> leafes)
        {
            TileType[][] lines = new TileType[60][];

            for (int i = 0; i < lines.GetLength(0); i++)
            {
                lines[i] = new TileType[120];
                for (int j = 0; j < 120; j++)
                    lines[i][j] = TileType.Wall;
            }

            foreach (var leaf in leafes)
            {
                if (leaf.Dungeon == null)
                    continue;
                Leaf d = leaf.Dungeon;
                for (int i = 0; i < d._height; i++)
                {
                    for (int j = 0; j < d._width; j++)

                        lines[d._top + i][d._left + j] = TileType.Floor;
                }
            }

            return lines;
        }
    }
}