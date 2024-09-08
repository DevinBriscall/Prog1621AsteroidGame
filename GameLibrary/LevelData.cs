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
                                new Dictionary<string, int> { { "size", 20 }, { "left", 130 }, { "top", 180 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 150 }, { "top", 80 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 300 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 450 }, { "top", 400 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 80 }, { "left", 200 }, { "top", 250 } },
                                new Dictionary<string, int> { { "size", 60 }, { "left", 350 }, { "top", 300 } }
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
                                new Dictionary<string, int> { { "size", 200 }, { "left", 300 }, { "top", 50 } },
                                new Dictionary<string, int> { { "size", 140 }, { "left", 100 }, { "top", 400 } },
                                new Dictionary<string, int> { { "size", 30 }, { "left", 500 }, { "top", 350 } },
                                new Dictionary<string, int> { { "size", 40 }, { "left", 400 }, { "top", 300 } }
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
                                new Dictionary<string, int> { { "size", 20 }, { "left", 200 }, { "top", 350 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 350 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 500 }, { "top", 300 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 50 }, { "left", 150 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 50 }, { "left", 300 }, { "top", 250 } },
                                new Dictionary<string, int> { { "size", 150 }, { "left", 200 }, { "top", 400 } }
                            }
                        }
                    }
                },
                {
                    4, new Dictionary<string, List<Dictionary<string, int>>>
                    {
                        {
                            "stars", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 20 }, { "left", 80 }, { "top", 80 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 180 }, { "top", 120 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 280 }, { "top", 180 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 380 }, { "top", 240 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 480 }, { "top", 300 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 30 }, { "left", 100 }, { "top", 100 } },
                                new Dictionary<string, int> { { "size", 130 }, { "left", 300 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 40 }, { "left", 450 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 100 }, { "left", 200 }, { "top", 400 } },
                                new Dictionary<string, int> { { "size", 50 }, { "left", 400 }, { "top", 350 } }
                            }
                        }
                    }
                },
                {
                    5, new Dictionary<string, List<Dictionary<string, int>>>
                    {
                        {
                            "stars", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 20 }, { "left", 100 }, { "top", 50 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 200 }, { "top", 100 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 300 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 400 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 500 }, { "top", 250 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 250 }, { "top", 300 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 30 }, { "left", 150 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 30 }, { "left", 300 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 40 }, { "left", 450 }, { "top", 250 } },
                                new Dictionary<string, int> { { "size", 300 }, { "left", 200 }, { "top", 400 } },
                                new Dictionary<string, int> { { "size", 50 }, { "left", 400 }, { "top", 350 } },
                                new Dictionary<string, int> { { "size", 40 }, { "left", 300 }, { "top", 100 } }
                            }
                        }
                    }
                },
                {
                    6, new Dictionary<string, List<Dictionary<string, int>>>
                    {
                        {
                            "stars", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 20 }, { "left", 120 }, { "top", 60 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 220 }, { "top", 110 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 320 }, { "top", 160 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 520 }, { "top", 260 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 250 }, { "top", 310 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 350 }, { "top", 360 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 30 }, { "left", 100 }, { "top", 100 } },
                                new Dictionary<string, int> { { "size", 40 }, { "left", 250 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 50 }, { "left", 400 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 60 }, { "left", 150 }, { "top", 350 } },
                                new Dictionary<string, int> { { "size", 70 }, { "left", 300 }, { "top", 300 } },
                                new Dictionary<string, int> { { "size", 60 }, { "left", 450 }, { "top", 250 } }
                            }
                        }
                    }
                },
                {
                    7, new Dictionary<string, List<Dictionary<string, int>>>
                    {
                        {
                            "stars", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 20 }, { "left", 150 }, { "top", 70 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 250 }, { "top", 120 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 350 }, { "top", 170 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 550 }, { "top", 270 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 300 }, { "top", 320 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 400 }, { "top", 370 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 500 }, { "top", 420 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 40 }, { "left", 150 }, { "top", 100 } },
                                new Dictionary<string, int> { { "size", 50 }, { "left", 300 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 60 }, { "left", 450 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 70 }, { "left", 200 }, { "top", 350 } },
                                new Dictionary<string, int> { { "size", 80 }, { "left", 400 }, { "top", 300 } },
                                new Dictionary<string, int> { { "size", 70 }, { "left", 300 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 60 }, { "left", 100 }, { "top", 300 } }
                            }
                        }
                    }
                },
                {
                    8, new Dictionary<string, List<Dictionary<string, int>>>
                    {
                        {
                            "stars", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 20 }, { "left", 80 }, { "top", 80 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 180 }, { "top", 120 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 280 }, { "top", 160 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 380 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 480 }, { "top", 240 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 150 }, { "top", 300 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 250 }, { "top", 350 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 350 }, { "top", 400 } },
                                new Dictionary<string, int> { { "size", 20 }, { "left", 450 }, { "top", 450 } }
                            }
                        },
                        {
                            "asteroids", new List<Dictionary<string, int>>
                            {
                                new Dictionary<string, int> { { "size", 30 }, { "left", 100 }, { "top", 150 } },
                                new Dictionary<string, int> { { "size", 40 }, { "left", 250 }, { "top", 200 } },
                                new Dictionary<string, int> { { "size", 50 }, { "left", 400 }, { "top", 250 } },
                                new Dictionary<string, int> { { "size", 60 }, { "left", 150 }, { "top", 350 } },
                                new Dictionary<string, int> { { "size", 70 }, { "left", 300 }, { "top", 300 } },
                                new Dictionary<string, int> { { "size", 80 }, { "left", 450 }, { "top", 250 } },
                                new Dictionary<string, int> { { "size", 70 }, { "left", 200 }, { "top", 150 } }
                            }
                        }
                    }
                },
                
            };
        }
    }
}
