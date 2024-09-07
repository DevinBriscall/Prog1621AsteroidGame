using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class LevelData
    {
        public Dictionary<int, Dictionary<string, List<Dictionary<string, int>>>> Levels { get; private set; }

        public LevelData()
        {
            Levels = new Dictionary<int, Dictionary<string, List<Dictionary<string, int>>>>
            {
                {
                    1, new Dictionary<string, List<Dictionary<string, int>>>
                    {
                        {
                            "stars", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 20 }, { "left", 50 }, { "top", 50 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 150 }, { "top", 80 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 300 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 450 }, { "top", 400 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 30 }, { "left", 200 }, { "top", 250 } },
                                new Dictionary<string, int> { { "size", 30 }, { "left", 350 }, { "top", 300 } }
                            }
                        }
                    }
                },
                {
                    2, new Dictionary<string, List<Dictionary<string, int>>>
                    {
                        {
                            "stars", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 20 }, { "left", 100 }, { "top", 100 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 200 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 300 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 400 }, { "top", 250 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 30 }, { "left", 300 }, { "top", 50 } },
                                new Dictionary<string, int> { { "size", 30 }, { "left", 100 }, { "top", 400 } },
                                new Dictionary<string, int> { { "size", 30 }, { "left", 500 }, { "top", 350 } }
                            }
                        }
                    }
                },
                {
                    3, new Dictionary<string, List<Dictionary<string, int>>>
                    {
                        {
                            "stars", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 20 }, { "left", 50 }, { "top", 50 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 200 }, { "top", 100 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 350 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 500 }, { "top", 300 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 30 }, { "left", 150 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 30 }, { "left", 300 }, { "top", 250 } }
                            }
                        }
                    }
                }
            };
        }
    }
}
