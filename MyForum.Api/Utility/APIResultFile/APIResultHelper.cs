using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.JWT.APIResultFile
{
    public static class APIResultHelper {
        //成功后返回的数据
        public static APIResult Success(dynamic data) {
            return new APIResult {
                Code = 200,
				Detail = data,
				Description = "操作成功"
            };
        }
		public static APIResult Success()
		{
			return new APIResult {
				Code = 200,
				Detail = null,
				Description = "操作成功"
			};
		}
		public static APIResult Error(string msg) {
            return new APIResult {
                Code = 500,
				Detail = null,
				Description = msg
            };
        }
    }
}
