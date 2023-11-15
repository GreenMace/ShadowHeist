using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public struct Vector2I
{
	public int x;
	public int y;
	public float d;

	public Vector2I(int nx, int ny, float nd = 1f)
	{
		x = nx;
		y = ny;
		d = nd;
	}
}

public interface GridData {
	int Width { get; }
	int Height { get; }
	float GetValue(int x, int y);
}

public class InfluenceMap : GridData
{
	List<Propagator> _propagators = new List<Propagator>();
	List<List<List<Vector2I>>> neighbours = new List<List<List<Vector2I>>>();


	float[,] _influences;
	float[,] _influencesBuffer;

	public float Decay { get; set; }
	public float Momentum { get; set; }

	public int Width { get { return _influences.GetLength(0); } }
	public int Height { get { return _influences.GetLength(1); } }

	public Vector3 origin = Vector3.zero;
	public float gridsize;
	public Transform bottomLeft;

	public InfluenceMap(int width, int height, float decay, float momentum)
	{
		_influences = new float[width, height];
		_influencesBuffer = new float[width, height];
		Decay = decay;
		Momentum = momentum;
	}

	public float GetValue(int x, int y)
	{
		return _influences[x, y];
	}

	public void SetUpNeighbours(Tilemap obstacles)
    {
		for (int x = 0; x < _influences.GetLength(0); ++x)
        {
			for (int y = 0; y < _influences.GetLength(1); ++y)
            {
				if (obstacles.HasTile(GetWorldPosition(new Vector2I(x, y, 1))))
                {
					continue;
                }

				List<Vector2I> retVal = new List<Vector2I>();

				if (x > 0 && !obstacles.HasTile(GetWorldPosition(new Vector2I(x-1, y, 1)))) {
					retVal.Add(new Vector2I(x - 1, y, 1));
				}
				
				if (x < _influences.GetLength(0) - 1 && !obstacles.HasTile(GetWorldPosition(new Vector2I(x + 1, y)))) retVal.Add(new Vector2I(x + 1, y));
				if (y > 0 && !obstacles.HasTile(GetWorldPosition(new Vector2I(x, y - 1)))) retVal.Add(new Vector2I(x, y - 1));
				if (y < _influences.GetLength(1) - 1 && !obstacles.HasTile(GetWorldPosition(new Vector2I(x, y + 1)))) retVal.Add(new Vector2I(x, y + 1));

				// diagonals
				if (x > 0 && y > 0 && !obstacles.HasTile(GetWorldPosition(new Vector2I(x - 1, y - 1, 1.4142f)))) retVal.Add(new Vector2I(x - 1, y - 1, 1.4142f));
				if (x < _influences.GetLength(0) - 1 && y < _influences.GetLength(1) - 1 && !obstacles.HasTile(GetWorldPosition(new Vector2I(x + 1, y + 1, 1.4142f)))) retVal.Add(new Vector2I(x + 1, y + 1, 1.4142f));
				if (x > 0 && y < _influences.GetLength(1) - 1 && !obstacles.HasTile(GetWorldPosition(new Vector2I(x - 1, y + 1, 1.4142f)))) retVal.Add(new Vector2I(x - 1, y + 1, 1.4142f));
				if (x < _influences.GetLength(0) - 1 && y > 0 && !obstacles.HasTile(GetWorldPosition(new Vector2I(x + 1, y - 1, 1.4142f)))) retVal.Add(new Vector2I(x + 1, y - 1, 1.4142f));

				neighbours[x][y] = retVal;
			}
        }
	}

	public Vector3Int GetWorldPosition(Vector2I pos) {
		Vector3 temp = new Vector3(pos.x, pos.y, 0) * gridsize;
		Vector3 worldPos = origin + temp;
		return Vector3Int.RoundToInt(worldPos);
	}

    public Vector2I GetGridPosition(Vector3 pos) {
		int x = (int)((pos.x - bottomLeft.position.x) / gridsize);
        int y = (int)((pos.y - bottomLeft.position.y) / gridsize);
		return new Vector2I(x, y, 1f);
    }

    public void SetInfluence(int x, int y, float value)
	{
		if (x < Width && y < Height)
		{
			_influences[x, y] = value;
			_influencesBuffer[x, y] = value;
		}
	}

	public void SetInfluence(Vector2I pos, float value)
	{
		if (pos.x < Width && pos.y < Height)
		{
			_influences[pos.x, pos.y] = value;
			_influencesBuffer[pos.x, pos.y] = value;
		}
	}

	public void RegisterPropagator(Propagator p)
	{
		_propagators.Add(p);
	}

	public void DeletePropagator(Propagator p)
	{
		_propagators.Remove(p);
	}

	public void Propagate()
	{
		UpdatePropagators();
		UpdatePropagation();
		UpdateInfluenceBuffer();
	}

	void UpdatePropagators()
	{
		for (int i = 0; i < _propagators.Count; ++i)
			SetInfluence(GetGridPosition(_propagators[i].transform.position), _propagators[i].Value);
	}

	void UpdatePropagation()
	{
		for (int xIdx = 0; xIdx < _influences.GetLength(0); ++xIdx)
		{
			for (int yIdx = 0; yIdx < _influences.GetLength(1); ++yIdx)
			{
				float maxInf = -1;
				float minInf = 1;
				Vector2I[] neighbors = GetNeighbors(xIdx, yIdx);
				foreach (Vector2I n in neighbors)
				{
					float inf = _influencesBuffer[n.x, n.y] * Mathf.Exp(-Decay * n.d);
					maxInf = Mathf.Max(inf, maxInf);
					minInf = Mathf.Min(inf, minInf);
				}

				if (Mathf.Abs(minInf) > maxInf)
					_influences[xIdx, yIdx] = Mathf.Lerp(_influencesBuffer[xIdx, yIdx], minInf, Momentum);
				else
					_influences[xIdx, yIdx] = Mathf.Lerp(_influencesBuffer[xIdx, yIdx], maxInf, Momentum);
			}
		}
	}

	void UpdateInfluenceBuffer()
	{
		for (int xIdx = 0; xIdx < _influences.GetLength(0); ++xIdx)
			for (int yIdx = 0; yIdx < _influences.GetLength(1); ++yIdx)
				_influencesBuffer[xIdx, yIdx] = _influences[xIdx, yIdx];
	}

	Vector2I[] GetNeighbors(int x, int y)
	{
		List<Vector2I> retVal = neighbours[x][y];
		return retVal.ToArray();
	}
}