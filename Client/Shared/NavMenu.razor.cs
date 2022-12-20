using blazor_wasm.Client.Service;
using EntityContext.Fms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.ApiResult;

namespace blazor_wasm.Client.Shared
{
    public partial class NavMenu
    {
        [CascadingParameter]
        public JsProviderService? JsProviderService { get; set; }

        [CascadingParameter]
        public MatIconProviderService? MatIconProviderService { get; set; }
        [CascadingParameter]
        public HttpService? HttpService { get; set; }

        [CascadingParameter]
        public ISnackbar? Snackbar { get; set; }

        private List<MenuItem>? _menus { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (HttpService is not null)
                {
                    var mineHttpResult = await HttpService.GetAsync("", $"/menu-item/get");
                    if (mineHttpResult is null) throw new Exception($"api call fail /menu/get");
                    if (mineHttpResult.IsSuccessStatusCode.Equals(false)) throw new Exception($"api call fail {mineHttpResult.StatusCode}");
                    var result = JsonSerialize.DeSerializeDefault<HttpResult<List<MenuItem>>>(await mineHttpResult.Content.ReadAsStringAsync());
                    if (result is null) throw new Exception($"api result error");
                    if (result.Success.Equals(false)) throw new Exception($"result fail {result.Message}");
                    if (result.Result is not null)
                    {
                        var lists = result.Result;
                        if (lists is not null)
                        {
                            _menus = lists.OrderBy(x => x.Prio).ToList();
                            SetRecursionMenus();
                        }
                    }
                    else
                    {
                        throw new Exception($"result not found");
                    }
                }
            }
            catch (Exception exception)
            {
                if (Snackbar is not null)
                {
                    Snackbar.Add(exception.Message);
                }
                else
                {
                    Console.WriteLine(exception.Message);
                }
            }
            
            await base.OnInitializedAsync();
        }

        private void SetRecursionMenus()
        {
            if (_menus is null) return;
            foreach (var menu in _menus)
            {
                SetRecursionMenu(menu, _menus);
            }
        }

        private void SetRecursionMenu(MenuItem menu, List<MenuItem> originMenus)
        {
            if (menu.Childs is null) menu.Childs = new();
            var adds = originMenus.Where(x => x.ParentId == menu.Id).ToList();
            if (adds is not null && adds.Count > 0)
            {
                menu.Childs.AddRange(adds);
            }
            foreach (var child in menu.Childs)
            {
                SetRecursionMenu(child, originMenus);
            }
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender == true)
            {
                if (JsProviderService is not null) await JsProviderService.ShowLoading();
                await WorkFirstRender();
                if (JsProviderService is not null) await JsProviderService.HideLoading();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task WorkFirstRender()
        {
            await Task.Delay(0);
        }

        public void Reload()
        {
            StateHasChanged();
        }
    }
}
