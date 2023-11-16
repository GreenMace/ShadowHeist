using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour {
    MeshRenderer _meshRenderer;
    MeshFilter _meshFilter;
    Mesh _mesh;

    GridData[] _data;

    [SerializeField] Material _material;
    [SerializeField] Color _neutralColor = Color.black;
    [SerializeField] Color _positiveColor = Color.white;

    Color[] _colors;

    public void SetGridData(GridData[] m) {
        _data = m;
    }

    public void CreateMesh(Vector3 bottomLeftPos, float gridSize) {
        _mesh = new Mesh();
        _meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        _meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        _meshFilter.mesh = _mesh;
        _meshRenderer.material = _material;

        _meshRenderer.sortingOrder = 5;

        float _startX = 0;
        float _startY = 0;

        List<Vector3> verts = new List<Vector3>();
        for (int yIdx = 0; yIdx < _data[0].Height; ++yIdx) {
            for (int xIdx = 0; xIdx < _data[0].Width; ++xIdx) {
                Vector3 bl = new Vector3(_startX + xIdx * gridSize, _startY + yIdx * gridSize, 0);
                Vector3 br = new Vector3(_startX + (xIdx + 1) * gridSize, _startY + yIdx * gridSize, 0);
                Vector3 tl = new Vector3(_startX + xIdx * gridSize, _startY + (yIdx + 1) * gridSize, 0);
                Vector3 tr = new Vector3(_startX + (xIdx + 1) * gridSize, _startY + (yIdx + 1) * gridSize, 0);

                verts.Add(bl);
                verts.Add(br);
                verts.Add(tl);
                verts.Add(tr);
            }
        }

        List<Color> colors = new List<Color>();
        for (int yIdx = 0; yIdx < _data[0].Height; ++yIdx) {
            for (int xIdx = 0; xIdx < _data[0].Width; ++xIdx) {
                colors.Add(Color.white);
                colors.Add(Color.white);
                colors.Add(Color.white);
                colors.Add(Color.white);
            }
        }

        _colors = colors.ToArray();

        List<Vector3> norms = new List<Vector3>();
        for (int yIdx = 0; yIdx < _data[0].Height; ++yIdx) {
            for (int xIdx = 0; xIdx < _data[0].Width; ++xIdx) {
                norms.Add(Vector3.up);
                norms.Add(Vector3.up);
                norms.Add(Vector3.up);
                norms.Add(Vector3.up);
            }
        }

        List<Vector2> uvs = new List<Vector2>();
        for (int yIdx = 0; yIdx < _data[0].Height; ++yIdx) {
            for (int xIdx = 0; xIdx < _data[0].Width; ++xIdx) {
                uvs.Add(new Vector2(0, 0));
                uvs.Add(new Vector2(1, 0));
                uvs.Add(new Vector2(0, 1));
                uvs.Add(new Vector2(1, 1));
            }
        }

        List<int> triangles = new List<int>();
        for (int idx = 0; idx < verts.Count; idx+=4) {
            int bl = idx;
            int br = idx + 1;
            int tl = idx + 2;
            int tr = idx + 3;

            triangles.Add(bl);
            triangles.Add(tl);
            triangles.Add(br);
            
            triangles.Add(tl);
            triangles.Add(tr);
            triangles.Add(br);
        }

        _mesh.vertices = verts.ToArray();
        _mesh.normals = norms.ToArray();
        _mesh.uv = uvs.ToArray();
        _mesh.colors = _colors;
        _mesh.triangles = triangles.ToArray();
    }

    public void SetColor(int x, int y, Color c) {
        int idx = ((y * _data[0].Width) + x) * 4;
        _colors[idx] = c;
        _colors[idx+1] = c;
        _colors[idx+2] = c;
        _colors[idx+3] = c;
    }

    // Update is called once per frame
    void Update()
    {
        if (_data != null) {
            for (int yIdx = 0; yIdx < _data[0].Height; ++yIdx) {
                for (int xIdx = 0; xIdx < _data[0].Width; ++xIdx) {
                    float value = 0;
                    float max_val = 0;
                    for (int i = 0; i < _data.Length; ++i) {
                        if (Mathf.Abs(_data[i].GetValue(xIdx, yIdx)) > max_val) {
                            max_val = Mathf.Abs(_data[i].GetValue(xIdx, yIdx));
                            value = _data[i].GetValue(xIdx, yIdx);
                        }
                    }

                    Color c = Color.Lerp(_neutralColor, _positiveColor, value);
                    
                    SetColor(xIdx, yIdx, c);
                }
            }
        }
        _mesh.colors = _colors;
    }
}
