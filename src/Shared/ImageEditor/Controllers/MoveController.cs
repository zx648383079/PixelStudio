using System.Linq;
using ZoDream.Shared.ImageEditor.Layers;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    /// <summary>
    /// 移动图层工具
    /// </summary>
    /// <param name="editor"></param>
    public class MoveController(IImageEditor editor) : ICommandController, IMouseState
    {
        private IMouseState? _currentState;

        public bool IsEnabled => true;

        public void Initialize(IImageLayer[] items)
        {
            if (items.Length == 0)
            {
                Dispose();
                return;
            }
            if (_currentState is not ResizeLayer)
            {
                Dispose();
            }
            _currentState ??= new ResizeLayer(editor);
            if (_currentState is ICommandLayer l)
            {
                l.Initialize(items);
            }
            editor.Invalidate();
        }

        public void Touch(Point point)
        {
            if (editor.Layer.TryGet(point, out var layer))
            {
                Dispose();
                var next = new ResizeLayer(editor);
                _currentState = next;
                next.Initialize([layer]);
                editor.Invalidate();
            }
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
            if (args.State.HasFlag(PointerState.Released) || _currentState is null)
            {
                return;
            }
            _currentState?.PointerMoved(args);
            editor.Invalidate();
        }

        public void PointerPressed(IMouseRoutedArgs args)
        {
            if (!args.IsLeftButtonPressed)
            {
                return;
            }
            _currentState ??= new SelectLayer();
            _currentState?.PointerPressed(args);
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
            if (args.State.HasFlag(PointerState.NotMoved))
            {
                Touch(args.Position);
                return;
            }
            _currentState?.PointerReleased(args);
            if (_currentState is SelectLayer layer)
            {
                var items = editor.Layer.Get(layer.Bound).ToArray();
                if (items.Length > 0)
                {
                    Dispose();
                    var next = new ResizeLayer(editor);
                    _currentState = next;
                    next.Initialize(items);
                }
            }
            editor.Invalidate();
        }

        public void Paint(IImageCanvas canvas)
        {
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
            _currentState = null;
        }


    }
}
