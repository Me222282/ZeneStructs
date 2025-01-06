namespace Zene.Structs
{
    public class OrthographicMatrix : IMatrix, ISizeable
    {
        public OrthographicMatrix() { }
        public OrthographicMatrix(floatv width, floatv height, floatv depthNear, floatv depthFar)
        {
            Width = width;
            Height = height;
            _depthN = depthNear;
            DepthFar = depthFar;
        }

        public int Rows => 4;
        public int Columns => 4;
        
        public bool Constant => false;
        
        private floatv _width;
        public floatv Width
        {
            get => _width;
            set
            {
                _width = value;
                _v1 = 1 / value;
            }
        }
        private floatv _height;
        public floatv Height
        {
            get => _height;
            set
            {
                _height = value;
                _v2 = 1 / value;
            }
        }
        public Vector2 Size
        {
            get => new Vector2(_width, _height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }
        private floatv _depthN;
        public floatv DepthNear
        {
            get => _depthN;
            set
            {
                _depthN = value;
                floatv invFN = 1 / (value - _depthF);
                _v3 = 2 * invFN;
                _v4 = (value + _depthF) * invFN;
            }
        }
        private floatv _depthF;
        public floatv DepthFar
        {
            get => _depthF;
            set
            {
                _depthF = value;
                floatv invFN = 1 / (_depthN - value);
                _v3 = 2 * invFN;
                _v4 = (value + _depthN) * invFN;
            }
        }
        
        private floatv _v1;
        private floatv _v2;
        private floatv _v3;
        private floatv _v4;

        Vector2I ISizeable.Size { set { Width = value.X; Height = value.Y; }  }

        public void MatrixData(MatrixSpan ms)
        {
            // 4x4 only
            if (ms.Rows != 4 || ms.Columns != 4)
            {
                ms.Padding(0, 0);
                return;
            }
            
            ms.Data[0] = _v1;
            ms.Data[5] = _v2;
            ms.Data[10] = _v3;
            ms.Data[14] = _v4;
            ms.Data[15] = 1;
        }
    }
}
