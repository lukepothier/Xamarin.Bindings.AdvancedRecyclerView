using System;
using System.Windows.Input;
using Android.Support.V7.Widget;
using Android.Views;
using Com.H6ah4i.Android.Widget.Advrecyclerview.Headerfooter;
using MvvmCross.AdvancedRecyclerView.Data;
using MvvmCross.AdvancedRecyclerView.TemplateSelectors;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Support.V7.RecyclerView.Model;
using Object = Java.Lang.Object;

namespace MvvmCross.AdvancedRecyclerView.Adapters
{
    public sealed class MvxHeaderFooterWrapperAdapter : AbstractHeaderFooterWrapperAdapter
    {
        public override int FooterItemCount => HeaderFooterDetails.HasFooter ? 1 : 0;
        public override int HeaderItemCount => HeaderFooterDetails.HasHeader ? 1 : 0;

        public MvxHeaderFooterDetails HeaderFooterDetails { get; set; } = new MvxHeaderFooterDetails();

        public MvxHeaderFooterWrapperAdapter(RecyclerView.Adapter baseAdapter) : this(baseAdapter, MvxAndroidBindingContextHelpers.Current())
        {

        }

        public MvxHeaderFooterWrapperAdapter(RecyclerView.Adapter baseAdapter, IMvxAndroidBindingContext bindingContext)
        {
            SetAdapter(baseAdapter);
            BindingContext = bindingContext;
        }

        private IMvxAndroidBindingContext BindingContext { get; }

        public override void OnBindFooterItemViewHolder(Object p0, int p1)
        {
            (p0 as IMvxRecyclerViewHolder).DataContext = BindingContext.DataContext;
            MvxFooterViewHolderBound?.Invoke(new MvxViewHolderBoundEventArgs(-1, BindingContext.DataContext, p0 as RecyclerView.ViewHolder));
        }

        public override void OnBindHeaderItemViewHolder(Object p0, int p1)
        {
            (p0 as IMvxRecyclerViewHolder).DataContext = BindingContext.DataContext;
            MvxHeaderViewHolderBound?.Invoke(new MvxViewHolderBoundEventArgs(-1, BindingContext.DataContext, p0 as RecyclerView.ViewHolder));
        }

        public override Object OnCreateFooterItemViewHolder(ViewGroup p0, int p1)
        {
            var itemBindingContext = new MvxAndroidBindingContext(p0.Context, BindingContext.LayoutInflaterHolder);

            var viewHolder =
                new MvxRecyclerViewHolder(
                    InflateViewForHolder(HeaderFooterDetails.FooterLayoutId, p0, p1, itemBindingContext),
                    itemBindingContext)
                {
                    Click = FooterClickCommand,
                    LongClick = FooterLongClickCommand,
                    DataContext = BindingContext.DataContext
                };

            return viewHolder;
        }

        public override Object OnCreateHeaderItemViewHolder(ViewGroup p0, int p1)
        {
            var itemBindingContext = new MvxAndroidBindingContext(p0.Context, BindingContext.LayoutInflaterHolder);

            var viewHolder =
                new MvxRecyclerViewHolder(
                    InflateViewForHolder(HeaderFooterDetails.HeaderLayoutId, p0, p1, itemBindingContext),
                    itemBindingContext)
                {
                    Click = HeaderClickCommand,
                    LongClick = HeaderLongClickCommand,
                    DataContext = BindingContext.DataContext
                };

            return viewHolder;
        }

        private View InflateViewForHolder(int headerLayoutId, ViewGroup p0, int p1, MvxAndroidBindingContext itemBindingContext)
            => itemBindingContext.BindingInflate(headerLayoutId, p0, false);

        public ICommand HeaderClickCommand { get; set; }
        public ICommand HeaderLongClickCommand { get; set; }

        public ICommand FooterClickCommand { get; set; }
        public ICommand FooterLongClickCommand { get; set; }

        public event Action<MvxViewHolderBoundEventArgs> MvxHeaderViewHolderBound;
		public event Action<MvxViewHolderBoundEventArgs> MvxFooterViewHolderBound;
    }
}