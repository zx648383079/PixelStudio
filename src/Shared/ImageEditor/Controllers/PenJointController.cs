using System.Collections.Generic;
using ZoDream.Shared.ImageEditor.Layers;
using ZoDream.Shared.ImageEditor.Sources;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    /// <summary>
    /// 路径节点编辑工具
    /// </summary>
    /// <param name="editor"></param>
    public class PenJointController(IImageEditor editor) : ICommandController, IMouseState
    {
        private IMouseState? _currentState;
        private PathImageSource? _layer;
        private Rect? _multipleRect;
        private IList<int> _selected = [];
        private Point _lastPoint = new();
        public bool IsEnabled => editor.Current?.Source is PathImageSource;

        public void Initialize(IImageLayer? layer)
        {
            _selected = [];
            if (layer?.Source is PathImageSource p)
            {
                _layer = p;
                return;
            }
            _layer = null;
        }

        public void Touch(Point point)
        {
            editor.Invalidate();
        }


        public void PointerPressed(IMouseRoutedArgs args)
        {
            if (_layer is null)
            {
                return;
            }
            _lastPoint = args.Position;
            if (_selected.Count > 1 && _multipleRect is not null)
            {
                if (_multipleRect.Value.Contains(_lastPoint))
                {
                    return;
                }
                _selected.Clear();
            }
            var index = _layer.NearOf(_lastPoint, editor.Options.JointSize / 2);
            if (index >= 0 && !_selected.Contains(index))
            {
                _selected = [index];
            }
            if (index < 0 && args.IsLeftButtonPressed)
            {
                _currentState ??= new SelectLayer();
                _currentState?.PointerPressed(args);
            }
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
            if (_layer is null || args.State.HasFlag(PointerState.Released))
            {
                return;
            }
            if (_selected.Count > 0)
            {
                var current = args.Position;
                var offset = current - _lastPoint;
                _layer?.Move([.. _selected], offset);
                if (_multipleRect is not null)
                {
                    _multipleRect = _multipleRect.Value + offset;
                }
                
                _lastPoint = current;
            } else
            {
                _currentState?.PointerMoved(args);
            }
            editor.Invalidate();
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
            if (args.State.HasFlag(PointerState.NotMoved))
            {
                Touch(args.Position);
                return;
            }
            if (_currentState is SelectLayer s && _layer is not null && s.IsVisible)
            {
                var rect = s.Bound;
                _selected.Clear();
                var path = _layer.Source;
                for (var i = 0; i < path.Count; i++)
                {
                    var p = path.GetPoint(i);
                    if (p is not null && rect.Contains(p.Value))
                    {
                        _selected.Add(i);
                    }
                }
                _multipleRect = rect;
            }
            _currentState?.PointerReleased(args);
            editor.Invalidate();
        }

        public void Paint(IImageCanvas canvas)
        {
            if (_layer is null || _layer.Source.IsEmpty)
            {
                return;
            }
            var options = editor.Options;
            var jointSize = options.JointSize;
            var jointHalf = jointSize / 2;
            var path = _layer.Source;
            for (int i = 0; i < path.Count; i++)
            {
                var item = path.GetPoint(i);
                if (item is null)
                {
                    continue;
                }
                var joint = new Rect(item.Value.X - jointHalf, item.Value.Y - jointHalf, jointSize, jointSize);
                canvas.DrawRect(joint,
                    _selected.Contains(i) ? options.JointHoveredPaint : options.JointPaint);
                canvas.DrawRect(joint, options.JointStrokePaint);
            }
            if (_currentState is ICommandLayer layer)
            {
                layer?.Paint(canvas);
            }
        }

        public void Dispose()
        {
            if (_currentState is ICommandLayer layer)
            {
                layer?.Dispose();
            }
        }

        
    }
}
