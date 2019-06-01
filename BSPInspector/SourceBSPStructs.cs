using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BSPInspector
{
    public static class SourceBSPStructs
    {
        public const int VBSP = 0x50534256;
        public const int GAMELUMP_STATIC_PROPS = 1936749168; // 'sprp';
        public static class MaxValues
        {
            public static int MAX_MAP_PLANES = 65536;
            public static int MAX_MAP_VERTS = 65536;
            public static int MAX_MAP_EDGES = 256000;
            public static int MAX_MAP_SURFEDGES = 512000;
            public static int MAX_MAP_FACES = 65536;
            public static int MAX_MAP_NODES = 65536;
            public static int MAX_MAP_LEAFFACES = 65536;
            public static int MAX_MAP_LEAFBRUSHES = 65536;
            public static int MAX_MAP_MODELS = 1024;
            public static int MAX_MAP_ENTITIES = 16384; //Depending on the Source engine version, the entity lump can contain 4096 (Source 2004) to 16384 (Alien Swarm) entities
            public static int MAX_MAP_CUBEMAPSAMPLES = 1024;
            public static int MAX_MAP_OVERLAYS = 512;
            public static int MAX_MAP_VISIBILITY = 0x1000000; //16 Mb, aw sheet
            public static int MAX_MAP_LIGHTING = 0x1000000; //16 Mb

            public static int MAX_MAP_BRUSHES = 8192;
            public static int MAX_MAP_BRUSHSIDES = 65536;
            public static int MAX_BRUSH_SIDES = 128;

            public static int MAX_MAP_TEXINFO = 12288;
            public static int MAX_MAP_TEXDATA = 2048;
            public static int MAX_MAP_TEXDATA_STRING_DATA = 256000; //bytes
            public static int TEXTURE_NAME_LENGTH = 128; //chars

            public static int MAX_KEY = 32;         //KV; chars
            public static int MAX_VALUE = 1024;     //KV; chars
        }

        public const int HEADER_LUMPS = 64;     //Num Lumps
        public enum Lumps
        {
            LUMP_ENTITIES    = 0,  //Map entities
            LUMP_PLANES      = 1,  //Plane array
            LUMP_TEXDATA     = 2,  //Index to texture names
            LUMP_VERTEXES    = 3,  //Vertex array
            LUMP_VISIBILITY  = 4,  //Compressed visibility bit arrays
            LUMP_NODES       = 5,  //BSP tree nodes
            LUMP_TEXINFO     = 6,  //Face texture array
            LUMP_FACES       = 7,  //Face array
            LUMP_LIGHTING    = 8,  //Lightmap samples
            LUMP_OCCLUSION   = 9,  //Occlusion polygons and vertices
            LUMP_LEAFS       = 10, //BSP tree leaf nodes
            LUMP_FACEIDS     = 11, //Correlates between dfaces and Hammer face IDs. Also used as random seed for detail prop placement.
            LUMP_EDGES       = 12, //Edge array
            LUMP_SURFEDGES   = 13, //Index of edges
            LUMP_MODELS      = 14, //Brush models (geometry of brush entities)
            LUMP_WORLDLIGHTS = 15, //Internal world lights converted from the entity lump
            LUMP_LEAFFACES   = 16, //Index to faces in each leaf
            LUMP_LEAFBRUSHES = 17, //Index to brushes in each leaf
            LUMP_BRUSHES     = 18, //Brush array
            LUMP_BRUSHSIDES  = 19, //Brushside array
            LUMP_AREAS       = 20, //Area array
            LUMP_AREAPORTALS = 21, //Portals between areas

            //different lumps in source 2004, 2007 and 2009, be careful
            //used lumps for source 2009
            LUMP_PROPCOLLISION = 22, //Static props convex hull lists
            LUMP_PROPHULLS     = 23, //Static prop convex hulls
            LUMP_PROPHULLVERTS = 24, //Static prop collision vertices
            LUMP_PROPTRIS      = 25, //Static prop per hull triangle index start/count
            //end of different lumps

            LUMP_DISPINFO          = 26, //Displacement surface array 
            LUMP_ORIGINALFACES     = 27, //Brush faces array before splitting
            LUMP_PHYSDISP          = 28, //Displacement physics collision data
            LUMP_PHYSCOLLIDE       = 29, //Physics collision data
            LUMP_VERTNORMALS       = 30, //Face plane normals
            LUMP_VERTNORMALINDICES = 31, //Face plane normal index array

            LUMP_DISP_LIGHTMAP_ALPHAS           = 32, //Displacement lightmap alphas (unused/empty since Source 2006) 
            LUMP_DISP_VERTS                     = 33, //Vertices of displacement surface meshes 
            LUMP_DISP_LIGHTMAP_SAMPLE_POSITIONS = 34, //Displacement lightmap sample positions 

            LUMP_GAME_LUMP                 = 35, //Game-specific data lump 
            LUMP_LEAFWATERDATA             = 36, //Data for leaf nodes that are inside water
            LUMP_PRIMITIVES                = 37, //Water polygon data
            LUMP_PRIMVERTS                 = 38, //Water polygon vertices
            LUMP_PRIMINDICES               = 39, //Water polygon vertex index array
            LUMP_PAKFILE                   = 40, //Embedded uncompressed Zip-format file 
            LUMP_CLIPPORTALVERTS           = 41, //Clipped portal polygon vertices
            LUMP_CUBEMAPS                  = 42, //env_cubemap location array
            LUMP_TEXDATA_STRING_DATA       = 43, //Texture name data
            LUMP_TEXDATA_STRING_TABLE      = 44, //Index array into texdata string data
            LUMP_OVERLAYS                  = 45, //info_overlay data array
            LUMP_LEAFMINDISTTOWATER        = 46, //Distance from leaves to water
            LUMP_FACE_MACRO_TEXTURE_INFO   = 47, //Macro texture info for faces
            LUMP_DISP_TRIS                 = 48, //Displacement surface triangles 
            LUMP_PROP_BLOB                 = 49, //Static prop triangle and string data    //DIFFERENT IN SOURCE 2004!!!
            LUMP_WATEROVERLAYS             = 50, //info_overlay's on water faces?
            LUMP_LEAF_AMBIENT_INDEX_HDR    = 51, //Index of LUMP_LEAF_AMBIENT_LIGHTING_HDR //DIFFERENT IN SOURCE 2006!!!
            LUMP_LEAF_AMBIENT_INDEX        = 52, //Index of LUMP_LEAF_AMBIENT_LIGHTING     //DIFFERENT IN SOURCE 2006!!!
            LUMP_LIGHTING_HDR              = 53, //HDR lightmap samples 
            LUMP_WORLDLIGHTS_HDR           = 54, //Internal HDR world lights converted from the entity lump 
            LUMP_LEAF_AMBIENT_LIGHTING_HDR = 55, //Per-leaf ambient light samples (HDR) 
            LUMP_LEAF_AMBIENT_LIGHTING     = 56, //Per-leaf ambient light samples (LDR)
            LUMP_XZIPPAKFILE               = 57, //XZip version of pak file for Xbox. Deprecated.
            LUMP_FACES_HDR                 = 58, //HDR maps may have different face data
            LUMP_MAP_FLAGS                 = 59, //Extended level-wide flags. Not present in all levels.
            LUMP_OVERLAY_FADES             = 60, //Fade distances for overlays
            LUMP_OVERLAY_SYSTEM_LEVELS     = 61, //System level settings (min/max CPU & GPU to render this overlay)
            LUMP_PHYSLEVEL                 = 62, //unknown
            LUMP_DISP_MULTIBLEND           = 63  //Displacement multiblend info
        }
        public static int GetLump(Lumps lump) => (int)lump;
        public static Lumps GetLump(int index) => (Lumps)index;

        public enum Surfs
        {
            SURF_LIGHT = 0x0001,       // value will hold the light strength
            SURF_SKY2D = 0x0002,       // don't draw, indicates we should skylight + draw 2d sky but not draw the 3D skybox
            SURF_SKY = 0x0004,     // don't draw, but add to skybox
            SURF_WARP = 0x0008,// turbulent water warp
            SURF_TRANS = 0x0010,
            SURF_NOPORTAL = 0x0020,    // the surface can not have a portal placed on it
            SURF_TRIGGER = 0x0040, // FIXME: This is an xbox hack to work around elimination of trigger surfaces, which breaks occluders
            SURF_NODRAW = 0x0080,  // don't bother referencing the texture
            SURF_HINT = 0x0100,    // make a primary bsp splitter
            SURF_SKIP = 0x0200,    // completely ignore, allowing non-closed brushes
            SURF_NOLIGHT = 0x0400, // Don't calculate light
            SURF_BUMPLIGHT = 0x0800,   // calculate three lightmaps for the surface for bumpmapping
            SURF_NOSHADOWS = 0x1000,   // Don't receive shadows
            SURF_NODECALS = 0x2000,    // Don't receive decals
            SURF_NOCHOP = 0x4000,  // Don't subdivide patches on this surface 
            SURF_HITBOX = 0x8000	// surface is part of a hitbox
        }
        public enum Contents : int
        {
            CONTENTS_EMPTY = 0, //No contents
            CONTENTS_SOLID = 0x1, //an eye is never valid in a solid
            CONTENTS_WINDOW = 0x2, //translucent, but not watery (glass)
            CONTENTS_AUX = 0x4,
            CONTENTS_GRATE = 0x8, //alpha-tested "grate" textures. Bullets/sight pass through, but solids don't
            CONTENTS_SLIME = 0x10,
            CONTENTS_WATER = 0x20,
            CONTENTS_MIST = 0x40,
            CONTENTS_OPAQUE = 0x80, //block AI line of sight
            CONTENTS_TESTFOGVOLUME = 0x100, //things that cannot be seen through (may be non-solid though)
            CONTENTS_UNUSED = 0x200, //unused
            CONTENTS_UNUSED6 = 0x400, //unused
            CONTENTS_TEAM1 = 0x800, //per team contents used to differentiate collisions between players and objects on different teams
            CONTENTS_TEAM2 = 0x1000,
            CONTENTS_IGNORE_NODRAW_OPAQUE = 0x2000, //ignore CONTENTS_OPAQUE on surfaces that have SURF_NODRAW
            CONTENTS_MOVEABLE = 0x4000, //hits entities which are MOVETYPE_PUSH (doors, plats, etc.)
            CONTENTS_AREAPORTAL = 0x8000, //remaining contents are non-visible, and don't eat brushes
            CONTENTS_PLAYERCLIP = 0x10000,
            CONTENTS_MONSTERCLIP = 0x20000,
            CONTENTS_CURRENT_0 = 0x40000, //currents can be added to any other contents, and may be mixed
            CONTENTS_CURRENT_90 = 0x80000,
            CONTENTS_CURRENT_180 = 0x100000,
            CONTENTS_CURRENT_270 = 0x200000,
            CONTENTS_CURRENT_UP = 0x400000,
            CONTENTS_CURRENT_DOWN = 0x800000,
            CONTENTS_ORIGIN = 0x1000000, //removed before bsping an entity
            CONTENTS_MONSTER = 0x2000000, //should never be on a brush, only in game
            CONTENTS_DEBRIS = 0x4000000,
            CONTENTS_DETAIL = 0x8000000, //brushes to be added after vis leafs
            CONTENTS_TRANSLUCENT = 0x10000000, //auto set if any surface has trans
            CONTENTS_LADDER = 0x20000000,
            CONTENTS_HITBOX = 0x40000000
        }

        public struct dheader_t
        {
            public int ident;                // BSP file identifier
            public int version;              // BSP file version
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HEADER_LUMPS)]
            public lump_t[] lumps;//[HEADER_LUMPS];  // lump directory array
            public int mapRevision;          // the map's revision (iteration, version) number
        };

        public struct lump_t
        {
            public int fileofs;    // offset into file (bytes)
            public int filelen;    // length of lump (bytes)
            public int version;    // lump format version
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] fourCC; // lump ident code //4 chars

            public int GetCount<T>() => filelen / Marshal.SizeOf(default(T));
            public T[] Parse<T>(BinaryReader br)
            {
                int count = GetCount<T>();
                T[] ret = new T[count];
                br.BaseStream.Seek(fileofs, SeekOrigin.Begin);
                for (int i = 0; i < count; i++)
                {
                    ret[i] = Utils.ByteToType<T>(br);
                }
                return ret;
            }
            public string Read(BinaryReader br)
            {
                Encoding encoding = Encoding.ASCII;
                br.BaseStream.Seek(fileofs, SeekOrigin.Begin);
                return encoding.GetString(br.ReadBytes(filelen));
            }
        };

        public struct dplane_t //Lump 1
        {
            public Vector3 normal;  // normal vector
            public float dist; // distance from origin
            public int type;   // plane axis identifier
        };

        public struct dtexdata_t //Lump 2
        {
            public Vector3 reflectivity;        // RGB reflectivity
            public int nameStringTableID;  // index into TexdataStringTable
            public int width, height;      // source image
            public int view_width, view_height;
        };

        public struct dvertexes_t //Lump 3 //not documented.
        {
            public Vector3 vertexes;
        };

        public struct dnode_t //Lump 5
        {
            public int planenum;   // index into plane array
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] children;    // negative numbers are -(leafs + 1), not nodes
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public short[] mins;  // for frustum culling
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public short[] maxs;
            public short firstface;   // index into face array
            public short numfaces;    // counting both sides
            public short area;     // If all leaves below this node are in the same area, then
                                   // this is the area index. If not, this is -1.
            public short paddding; // pad to 32 bytes length
        };

        public struct texinfo_t //Lump 6
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Vector2[] textureVecs;	// [s/t][xyz offset]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Vector2[] lightmapVecs;	// [s/t][xyz offset] - length is in units of texels/area
            public int flags;          // miptex flags	overrides
            public int texdata;        // Pointer to texture name, size, etc.
        }

        public struct dface_t //Lump 7
        {
            public short planenum;        // the plane number
            public byte side;          // faces opposite to the node's plane direction
            public byte onNode;            // 1 of on node, 0 if in leaf
            public int firstedge;      // index into surfedges
            public short numedges;     // number of surfedges
            public short texinfo;      // texture info
            public short dispinfo;     // displacement info
            public short surfaceFogVolumeID;   // ?
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] styles;     // switchable lighting info
            public int lightofs;       // offset into lightmap lump
            public float area;         // face area in units^2
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] LightmapTextureMinsInLuxels; // texture lighting info
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] LightmapTextureSizeInLuxels; // texture lighting info
            public int origFace;       // original face this was split from
            public short numPrims;        // primitives
            public short firstPrimID;
            public int smoothingGroups;   // lightmap smoothing group
        };

        public struct ColorRGBExp32 //Lump 8 (LDR) + Lump 53 (HDR)
        {
            public byte r, g, b;
            public char exponent;
        };

        public struct dleaf_t //Lump 10
        {
            public int contents;       // OR of all brushes (not needed?)
            public short cluster;      // cluster this leaf is in
            public short area;//:9;           // area this leaf is in
            public short flags;//:7;      // flags
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public short[] mins;      // for frustum culling
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public short[] maxs;
            public short firstleafface;       // index into leaffaces
            public short numleaffaces;
            public short firstleafbrush;      // index into leafbrushes
            public short numleafbrushes;
            public short leafWaterDataID;  // -1 for not in water

            //!!! NOTE: for maps of version 19 or lower uncomment this block
            /*
            CompressedLightCube	ambientLighting;	// Precaculated light info for entities.
            short			padding;		// padding to 4-byte boundary
            */
        };

        public struct dedge_t //Lump 12
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public short[] v;    // vertex indices
        };

        public struct dsurfedge_t //Lump 13 //not documented. //not sure if it works properly
        {
            public int surfedge;
        }

        public struct dmodel_t //Lump 14
        {
            public Vector3 mins, maxs;      // bounding box
            public Vector3 origin;          // for sounds or lights
            public int headnode;       // index into node array
            public int firstface, numfaces;    // index into face array
        };

        public struct dleafface_t //Lump 16 //not documented.
        {
            public short leafface;
        }

        public struct dleafbrush_t //Lump 17 //not documented.
        {
            public short leafbrush;
        }

        public struct dbrush_t //Lump 18
        {
            public int firstside;  // first brushside
            public int numsides;   // number of brushsides
            public int contents;   // contents flags

            public string contents_flag => Utils.GetFlagsAsString<Contents>(contents);
        };

        public struct dbrushside_t //Lump 19
        {
            public short planenum;    // facing out of the leaf
            public short texinfo;  // texture info
            public short dispinfo; // displacement info
            public short bevel;        // is the side a bevel plane?
        };

        public struct ddispinfo_t //Lump 26
        {
            public Vector3 startPosition;       // start position used for orientation
            public int DispVertStart;      // Index into LUMP_DISP_VERTS.
            public int DispTriStart;       // Index into LUMP_DISP_TRIS.
            public int power;          // power - indicates size of surface (2^power	1)
            public int minTess;        // minimum tesselation allowed
            public float smoothingAngle;       // lighting smoothing angle
            public int contents;       // surface contents
            public short MapFace;     // Which map face this displacement comes from.
            public int LightmapAlphaStart; // Index into ddisplightmapalpha.
            public int LightmapSamplePositionStart;    // Index into LUMP_DISP_LIGHTMAP_SAMPLE_POSITIONS.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public CDispNeighbor[] EdgeNeighbors; // Indexed by NEIGHBOREDGE_ defines.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public CDispCornerNeighbors[] CornerNeighbors;    // Indexed by CORNER_ defines.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public int[] AllowedVerts;  // active verticies
        }

        public struct CDispNeighbor
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public CDispSubNeighbor[] m_SubNeighbors; //[2]
        }

        public struct CDispSubNeighbor
        {
            public ushort Neighbor_index;       // This indexes into ddispinfos.
                                                // 0xFFFF if there is no neighbor here.

            public char neighbor_orient;        // (CCW) rotation of the neighbor wrt this displacement.

            // These use the NeighborSpan type.
            public char local_span;                     // Where the neighbor fits onto this side of our displacement.
            public char neighbor_span;              // Where we fit onto our neighbor.
        }

        public struct CDispCornerNeighbors
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] neighbor_indices;//[MAX_DISP_CORNER_NEIGHBORS] 4	  indices of neighbors.
            public byte neighbor_count;
        }

        public struct dDispVert //Lump 33
        {
            public Vector3 vec; // Vector field defining displacement volume.
            public float dist; // Displacement distances.
            public float alpha;    // "per vertex" alpha values.
        };
    }
}
