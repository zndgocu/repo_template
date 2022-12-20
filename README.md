# 이 저장소는 그리드 및 페이지레이아웃을 빠르게 개발하기 위해 제작되었습니다.(this project is template for in order to create grid fastest)
[This Project .git https](https://scm.devops.doosan.com/daehyun.kim/vs_code-blazor.git)

# 작업내용
- 템플릿페이지가 제작되었습니다.
- 템플릿 페이지에 그리드 아이템이 추가되었습니다.
- 데이터베이스에 값을 채우면 페이지레이아웃을 그려줍니다.
- 데이터베이스에 값을 채우면 테이블을 그려줍니다.
- 테이블아이템에 엔티티타입과 api 주소를 설정하면 crud가 자동으로 구현됩니다.
- api 관련 모델 wrapper이 생성되었습니다.
- 모델 쿼리 생성 wrapper이 생성되었습니다.
- 모델 쿼리 생성 시 put 관련 수정이 필요합니다.

# 종속성(dependency)
- PC
- - dotnet 7 : [Download DotNet7](https://dotnet.microsoft.com/en-us/download)
- - vs-code : [Download vs-code](https://code.visualstudio.com/)
- vs-code extension
- - c#
- - blazor webassembly debug
- optional install
- - coding pack for vs-code 
- - extension for vs-code 
- - [Download](https://code.visualstudio.com/docs/languages/dotnet)

# 이렇게 따라하세요(how to use a git-bash)
1. gitbash 설치(install git bash on your computer)
- [Download GitBash](https://git-scm.com/downloads)

2. git-bash 설정(setting git-bash)
<pre>
<body>
git config --global user.name "@{your name}"
git config --global user.email "@{your email}"
</body>
</pre>

3. 로컬 리포지토리 위치 설정(change directory on the git-bash)
<pre>
<body>
cd @{localpath}
</body>
</pre>

4. git 복사하기(clonning to your directory)
<pre>
<body>
git clone https://scm.devops.doosan.com/daehyun.kim/vs_code-blazor.git
</body>
</pre>

5. 복사 데이터 확인하기(check clone repository)
- it must be watched branch name "main"
<pre>
<body>
1. cd vs_code-blazor
2. git branch
</pre>
</body>

6. 브랜치 생성하기(how to create branch)
<pre>
<body>
1. git branch @{branch name}
2. git checkout @{branch name}
3. git branch
- you should have name "@{branch name}"
</body>
</pre>

# vs-code에서 어떻게 수정합니까?(how to edit code on the vs-code)
1. vs-code를 여세요(open the vs-code)
2. 워크스페이스를 설정하세요(open work space from file)
- file path : @{localpath}/vs_code-blazor/blazor_wasm.code-workspace

# 페이지는 어떻게 구현합니까?(how to use - page)
1. 모델 코드를 작성하세요(gotta write code {model})
    ~~~
    public class RobotState : FmsWrapper
    {
        public RobotState() : base(typeof(RobotState))
        {
            RobotId = "";
        }

        protected override List<string>? KeyColumns => new List<string>(){
            "robot_id"
        };

        [JsonInclude]
        public string RobotId { get; set; }
        [JsonInclude]
        public string? TaskId { get; set; }
        [JsonInclude]
        public string? RobotMode { get; set; }
        [JsonInclude]
        public string? GoalNodeId { get; set; }
        [JsonInclude]
        public Decimal? PoseX { get; set; }
        [JsonInclude]
        public Decimal? PoseY { get; set; }
        [JsonInclude]
        public Decimal? PoseZ { get; set; }
        [JsonInclude]
        public Decimal? TargetX { get; set; }
        [JsonInclude]
        public Decimal? TargetY { get; set; }
        [JsonInclude]
        public Decimal? TargetZ { get; set; }
        [JsonInclude]
        public Decimal? Progress { get; set; }
        [JsonInclude]
        public Decimal? Battery { get; set; }
        [JsonInclude]
        public string? ReadDate { get; set; }
        [JsonInclude]
        public Decimal? PoseTheta { get; set; }
    }
    ~~~
2. api 코드를 작성하세요(gotta write code {api})
    ~~~
    [ApiController]
    [Route("menu-item")]
    public class MenuItemItemController : Controller
    {
        private readonly ILogger<MenuItemItemController> _logger;
        private readonly IQueryManager _queryManager;

        public MenuItemItemController(ILogger<MenuItemItemController> logger, IQueryManager queryManager)
        {
            _logger = logger;
            _queryManager = queryManager;
        }


        [HttpGet]
        [Route("get")]
        public HttpResult<List<MenuItem>> Get()
        {
            HttpResult<List<MenuItem>> httpResult = new();
            try
            {
                MenuItem rs = new MenuItem();
                string? qry = rs.GetReadQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetReadQuery()");
                }
                var result = _queryManager.ExcuteQuery<MenuItem>(qry);

                httpResult.Success = result.GetIsSucceed();
                httpResult.Result = result.Values;
                if (httpResult.Success == false)
                {
                    httpResult.Message = qry;
                }
            }
            catch (System.Exception exception)
            {
                httpResult.Success = false;
                httpResult.Message = exception.Message;
                httpResult.Result = null;
            }
            return httpResult;
        }

        [HttpPost]
        [Route("post")]
        public HttpResult<MenuItem> Post(MenuItem? request)
        {
            HttpResult<MenuItem> httpResult = new();
            try
            {
                if (request is null)
                {
                    throw new Exception("a request item is null");
                }

                string? qry = request.GetCreateQuery();
                httpResult.Message = qry;
                if (qry is null)
                {
                    throw new Exception("exception model GetCreateQuery()");
                }
                var result = _queryManager.ExcuteNonQuery(qry);

                httpResult.Success = result.GetIsSucceed();
                httpResult.Result = request;
                if (httpResult.Success == false)
                {
                    httpResult.Message = qry;
                }
            }
            catch (System.Exception exception)
            {
                httpResult.Success = false;
                httpResult.Message = exception.Message;
                httpResult.Result = null;
            }
            return httpResult;
        }


        [HttpPut]
        [Route("put")]
        public HttpResult<int> Put(MenuItem? request)
        {
            HttpResult<int> httpResult = new();
            try
            {
                if (request is null)
                {
                    throw new Exception("a request item is null");
                }

                string? qry = request.GetUpdateQuery();
                httpResult.Message = qry;
                if (qry is null)
                {
                    throw new Exception("exception model GetCreateQuery()");
                }
                var result = _queryManager.ExcuteNonQuery(qry);

                httpResult.Success = result.GetIsSucceed();
                httpResult.Result = result.ExcuteCnt;
                httpResult.Message = qry;
            }
            catch (System.Exception exception)
            {
                httpResult.Success = false;
                httpResult.Message = exception.Message;
                httpResult.Result = -1;
            }
            return httpResult;
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResult<int> Delete([FromQuery] String? id)
        {
            HttpResult<int> httpResult = new();
            try
            {
                if (id is null) throw new Exception("id not found");

                MenuItem mi = new MenuItem();
                mi.Id = id;
                string? qry = mi.GetDeleteQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetDeleteQuery()");
                }
                var result = _queryManager.ExcuteNonQuery(qry);

                httpResult.Success = result.GetIsSucceed();
                httpResult.Result = result.ExcuteCnt;
                if (httpResult.Success == false)
                {
                    httpResult.Message = qry;
                }
            }
            catch (System.Exception exception)
            {
                httpResult.Success = false;
                httpResult.Message = exception.Message;
                httpResult.Result = -1;
            }
            return httpResult;
        }
    }
    ~~~
3. 데이터베이스에 값을 추가하세요(gotta insert data to database(template_page_layout))
    ~~~ 
    id : key
	parent_id : if this frame is child, enter the id on parent_frame 
	is_wire_frame 
	is_flex_item 
	is_flex_row 
	is_flex_grow 
	is_flex_basis 
	is_shadow_box 
	show_header
	header 
	header_style 
	show_table 
	dense_table 
	hover_table 
	border_table
	stripe_table 
	header_table
	c_table_url /${api address}
	r_table_url /${api address}
	u_table_url /${api address}
	d_table_url /${api address}
	table_entity_type /${model name : pascal case}
    ~~~
---

## 메뉴는 어떻게 생성합니까?(how to use - menu)
1. 데이터베이스에 값을 만드세요(gotta insert data to database(menu_item))
    ~~~
    id : key
	parent_id : if this frame is child, enter the id on parent_menu 
	is_template_page : True
	href  
	prio 
	display_name 
    ~~~


## script(database)
1. template_page_layout
    ~~~
    CREATE TABLE fms.template_page_layout (
	id varchar(36) NOT NULL,
	parent_id varchar(36) NULL,
	is_wire_frame bool NULL,
	is_flex_item bool NULL,
	is_flex_row bool NULL,
	is_flex_grow bool NULL,
	is_flex_basis bool NULL,
	is_shadow_box bool NULL,
	show_header bool NULL,
	"header" varchar NULL,
	header_style varchar NULL,
	show_table bool NULL,
	dense_table bool NULL,
	hover_table bool NULL,
	border_table bool NULL,
	stripe_table bool NULL,
	header_table varchar NULL,
	c_table_url varchar NULL,
	r_table_url varchar NULL,
	u_table_url varchar NULL,
	d_table_url varchar NULL,
	table_entity_type varchar NULL,
	CONSTRAINT template_page_layout_pk PRIMARY KEY (id));
    ~~~
2. menu_item
    ~~~
    CREATE TABLE fms.menu_item (
	id varchar NOT NULL,
	parent_id varchar NULL,
	is_template_page bool NULL,
	href varchar NULL,
	prio int4 NULL,
	display_name varchar NOT NULL,
	CONSTRAINT menu_item_pk PRIMARY KEY (id));
    ~~~
