using System.ComponentModel.DataAnnotations;

namespace AspNetCore.ToDo.Models {
    //通过模型绑定(model binding) 的流程，ASP.NET Core 把 POST 请求里面的参数，跟你创建的模型定义进行配对。
    //如果参数名匹配(忽略诸如大小写这种因素)，请求数据将被置入到模型里
    public class NewTodoItem {
        [Required]
        public string Title { get; set; }
    }
}