using System;
using System.Runtime.InteropServices;

namespace FMOD;

public class Geometry : HandleBase
{
	public RESULT release()
	{
		RESULT num = FMOD_Geometry_Release(getRaw());
		if (num == RESULT.OK)
		{
			rawPtr = IntPtr.Zero;
		}
		return num;
	}

	public RESULT addPolygon(float directocclusion, float reverbocclusion, bool doublesided, int numvertices, VECTOR[] vertices, out int polygonindex)
	{
		return FMOD_Geometry_AddPolygon(rawPtr, directocclusion, reverbocclusion, doublesided, numvertices, vertices, out polygonindex);
	}

	public RESULT getNumPolygons(out int numpolygons)
	{
		return FMOD_Geometry_GetNumPolygons(rawPtr, out numpolygons);
	}

	public RESULT getMaxPolygons(out int maxpolygons, out int maxvertices)
	{
		return FMOD_Geometry_GetMaxPolygons(rawPtr, out maxpolygons, out maxvertices);
	}

	public RESULT getPolygonNumVertices(int index, out int numvertices)
	{
		return FMOD_Geometry_GetPolygonNumVertices(rawPtr, index, out numvertices);
	}

	public RESULT setPolygonVertex(int index, int vertexindex, ref VECTOR vertex)
	{
		return FMOD_Geometry_SetPolygonVertex(rawPtr, index, vertexindex, ref vertex);
	}

	public RESULT getPolygonVertex(int index, int vertexindex, out VECTOR vertex)
	{
		return FMOD_Geometry_GetPolygonVertex(rawPtr, index, vertexindex, out vertex);
	}

	public RESULT setPolygonAttributes(int index, float directocclusion, float reverbocclusion, bool doublesided)
	{
		return FMOD_Geometry_SetPolygonAttributes(rawPtr, index, directocclusion, reverbocclusion, doublesided);
	}

	public RESULT getPolygonAttributes(int index, out float directocclusion, out float reverbocclusion, out bool doublesided)
	{
		return FMOD_Geometry_GetPolygonAttributes(rawPtr, index, out directocclusion, out reverbocclusion, out doublesided);
	}

	public RESULT setActive(bool active)
	{
		return FMOD_Geometry_SetActive(rawPtr, active);
	}

	public RESULT getActive(out bool active)
	{
		return FMOD_Geometry_GetActive(rawPtr, out active);
	}

	public RESULT setRotation(ref VECTOR forward, ref VECTOR up)
	{
		return FMOD_Geometry_SetRotation(rawPtr, ref forward, ref up);
	}

	public RESULT getRotation(out VECTOR forward, out VECTOR up)
	{
		return FMOD_Geometry_GetRotation(rawPtr, out forward, out up);
	}

	public RESULT setPosition(ref VECTOR position)
	{
		return FMOD_Geometry_SetPosition(rawPtr, ref position);
	}

	public RESULT getPosition(out VECTOR position)
	{
		return FMOD_Geometry_GetPosition(rawPtr, out position);
	}

	public RESULT setScale(ref VECTOR scale)
	{
		return FMOD_Geometry_SetScale(rawPtr, ref scale);
	}

	public RESULT getScale(out VECTOR scale)
	{
		return FMOD_Geometry_GetScale(rawPtr, out scale);
	}

	public RESULT save(IntPtr data, out int datasize)
	{
		return FMOD_Geometry_Save(rawPtr, data, out datasize);
	}

	public RESULT setUserData(IntPtr userdata)
	{
		return FMOD_Geometry_SetUserData(rawPtr, userdata);
	}

	public RESULT getUserData(out IntPtr userdata)
	{
		return FMOD_Geometry_GetUserData(rawPtr, out userdata);
	}

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_Release(IntPtr geometry);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_AddPolygon(IntPtr geometry, float directocclusion, float reverbocclusion, bool doublesided, int numvertices, VECTOR[] vertices, out int polygonindex);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetNumPolygons(IntPtr geometry, out int numpolygons);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetMaxPolygons(IntPtr geometry, out int maxpolygons, out int maxvertices);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetPolygonNumVertices(IntPtr geometry, int index, out int numvertices);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_SetPolygonVertex(IntPtr geometry, int index, int vertexindex, ref VECTOR vertex);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetPolygonVertex(IntPtr geometry, int index, int vertexindex, out VECTOR vertex);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_SetPolygonAttributes(IntPtr geometry, int index, float directocclusion, float reverbocclusion, bool doublesided);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetPolygonAttributes(IntPtr geometry, int index, out float directocclusion, out float reverbocclusion, out bool doublesided);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_SetActive(IntPtr geometry, bool active);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetActive(IntPtr geometry, out bool active);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_SetRotation(IntPtr geometry, ref VECTOR forward, ref VECTOR up);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetRotation(IntPtr geometry, out VECTOR forward, out VECTOR up);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_SetPosition(IntPtr geometry, ref VECTOR position);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetPosition(IntPtr geometry, out VECTOR position);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_SetScale(IntPtr geometry, ref VECTOR scale);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetScale(IntPtr geometry, out VECTOR scale);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_Save(IntPtr geometry, IntPtr data, out int datasize);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_SetUserData(IntPtr geometry, IntPtr userdata);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Geometry_GetUserData(IntPtr geometry, out IntPtr userdata);

	public Geometry(IntPtr raw)
		: base(raw)
	{
	}
}
