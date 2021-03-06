using System.Collections.Generic;
//using Sce.PlayStation.Core;
//using Sce.PlayStation.Core.Graphics;
using System.Linq;

using System;

namespace RulyModelConverter
{

	public class RenderList {
		public Material		material;

		public int			face_vert_offset;
		public int			face_vert_count;
		public int			bone_num;
		public byte[]   	weight;
		public int[]		bone_inv_map;
	}

	public class RenderBone {
		public Bone			bone;
		public float[]		matrix;
		public float[]		matrix_current;
		public double[]		quaternion;
		public bool updated;
	}
	
	public abstract class ShellSurface
	{
		public bool Loaded {
			get;
			set;
		}

		public bool Animation {
			get;
			set;
		}

		public string						ModelName	{ get; set; }
		public List<RenderList>				RenderLists	{ set; get; }
		public List<RenderBone>				RenderBones { set; get; }
		public IK[]							IK;
		public string[]						toon_name;

		public ShellSurface ()
		{
			RenderLists = new List<RenderList>();
			RenderBones = new List<RenderBone> ();
			Loaded = false;
		}

		public abstract void SetupShellSurface ();
	}
	
}
