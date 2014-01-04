using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulyModelConverter
{
    public class FMaterial
    {
        public float[] diffuse_color;
        public float power;
        public float[] specular_color;
        public float[] emmisive_color;
        public byte toon_index;
        public byte edge_flag;
        public int face_vert_count;
        public string texture;
        public string sphere;   // aditional

        // additional
        public int face_vert_offset;

        // additional for skinning
        public int bone_num;    // fixed(=48)
        public byte[] weight;
        public int[] bone_inv_map;
    }

    public class PMF
    {
        public bool is_pmf = false;
        public float[] VertNormUv;
        public ushort[] Index;
        public FMaterial[] Materials;
        public Bone[] Bones;
        public IK[] IKs;
        public string[] ToonNames;

        public PMF(PMD pmd)
        {
            CreateFromPMD(pmd);
        }

        private void CreateFromPMD(PMD pmd)
        {
            VertNormUv = new float[pmd.Vertex.Length / 3 * 8];
            for (int i = 0; i < pmd.Vertex.Length / 3; i++)
            {
                VertNormUv[i * 8 + 0] = pmd.Vertex[i * 3 + 0];
                VertNormUv[i * 8 + 1] = pmd.Vertex[i * 3 + 1];
                VertNormUv[i * 8 + 2] = pmd.Vertex[i * 3 + 2];
                VertNormUv[i * 8 + 3] = pmd.Normal[i * 3 + 0];
                VertNormUv[i * 8 + 4] = pmd.Normal[i * 3 + 1];
                VertNormUv[i * 8 + 5] = pmd.Normal[i * 3 + 2];
                VertNormUv[i * 8 + 6] = pmd.Uv[i * 2 + 0];
                VertNormUv[i * 8 + 7] = pmd.Uv[i * 2 + 1];
            }

            Index = new ushort[pmd.Index.Length];
            for (int i = 0; i < pmd.Index.Length; i++)
            {
                Index[i] = (ushort)pmd.Index[i];
            }

            Materials = new FMaterial[pmd.RenderLists.Count()];
            for (int i = 0; i < pmd.RenderLists.Count(); i++)
            {
                Materials[i].diffuse_color = pmd.RenderLists[i].material.diffuse_color;
                Materials[i].power = pmd.RenderLists[i].material.power;
                Materials[i].specular_color = pmd.RenderLists[i].material.specular_color;
                Materials[i].emmisive_color = pmd.RenderLists[i].material.emmisive_color;
                Materials[i].toon_index = pmd.RenderLists[i].material.toon_index;
                Materials[i].edge_flag = pmd.RenderLists[i].material.edge_flag;
                Materials[i].face_vert_count = pmd.RenderLists[i].face_vert_count;
                Materials[i].texture = pmd.RenderLists[i].material.texture;
                Materials[i].sphere = "";   // ad-hock
                Materials[i].face_vert_offset = pmd.RenderLists[i].face_vert_offset;
                Materials[i].bone_num = pmd.RenderLists[i].bone_num;
                Materials[i].weight = pmd.RenderLists[i].weight;
                Materials[i].bone_inv_map = pmd.RenderLists[i].bone_inv_map;
            }

            Bones = new Bone[pmd.RenderBones.Count()];
            for (int i = 0; i < pmd.RenderBones.Count(); i++)
            {
                Bones[i] = pmd.RenderBones[i].bone;
            }

            IKs = pmd.IK;
            ToonNames = pmd.toon_name;

            is_pmf = true;
        }

        public void Save(string filename)
        {
            using (var fs = File.OpenRead(filename))
            using (var bs = new BinaryWriter(fs))
            {
                // header
                bs.Write("PMF");
                bs.Write('\0');

                // Vertex, Normal, Uv
                bs.Write((uint)VertNormUv.Length);
                for (int i = 0; i < VertNormUv.Length; i++)
                {
                    bs.Write(VertNormUv[i]);
                }

                // Index
                bs.Write((uint)Index.Length);
                for (int i = 0; i < Index.Length; i++)
                {
                    bs.Write(Index[i]);
                }

                // Bones
                bs.Write((uint)Bones.Length);
                for (int i = 0; i < Bones.Length; i++)
                {
                    bs.Write(Bones[i].parent);
                    bs.Write(Bones[i].tail);
                    bs.Write(Bones[i].type);
                    bs.Write(Bones[i].ik);
                    bs.Write(Bones[i].head_pos[0]);
                    bs.Write(Bones[i].head_pos[1]);
                    bs.Write(Bones[i].head_pos[2]);
                    bs.Write(Bones[i].is_leg);
                }

                // IKs
                bs.Write((uint)IKs.Length);
                for (int i = 0; i < IKs.Length; i++)
                {
                    bs.Write(IKs[i].ik_bone_index);
                    bs.Write(IKs[i].ik_target_bone_index);
                    bs.Write(IKs[i].ik_chain_length);
                    bs.Write(IKs[i].iterations);
                    bs.Write(IKs[i].control_weight);
                    bs.Write(IKs[i].ik_child_bone_index.Length);
                    for (int j = 0; j < IKs[i].ik_child_bone_index.Length; j++)
                    {
                        bs.Write(IKs[i].ik_child_bone_index[j]);
                    }
                }

                // Materials
                bs.Write((uint)Materials.Length);
                for (int i = 0; i < Materials.Length; i++)
                {
                    bs.Write(Materials[i].diffuse_color[0]);
                    bs.Write(Materials[i].diffuse_color[1]);
                    bs.Write(Materials[i].diffuse_color[2]);
                    bs.Write(Materials[i].diffuse_color[3]);
                    bs.Write(Materials[i].power);
                    bs.Write(Materials[i].specular_color[0]);
                    bs.Write(Materials[i].specular_color[1]);
                    bs.Write(Materials[i].specular_color[2]);
                    bs.Write(Materials[i].emmisive_color[0]);
                    bs.Write(Materials[i].emmisive_color[1]);
                    bs.Write(Materials[i].emmisive_color[2]);
                    bs.Write(Materials[i].toon_index);
                    bs.Write(Materials[i].edge_flag);
                    bs.Write(Materials[i].face_vert_count);
                    bs.Write(Materials[i].face_vert_offset);
                    bs.Write(Materials[i].bone_num);
                    for (int j = 0; j < Materials[i].weight.Length; j++)
                    {
                        bs.Write(Materials[i].weight[i]);
                    }
                    for (int j = 0; j < Materials[i].bone_inv_map.Length; j++ )
                    {
                        bs.Write(Materials[i].bone_inv_map[j]);
                    }
                }
 
                // ToonNames
                for (int i = 0; i < 11; i++)
                {
                    bs.Write(ToonNames[i]);
                }
            }
        }
    }
}
