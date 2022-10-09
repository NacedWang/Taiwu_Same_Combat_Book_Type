return {
	Description = "秦始皇统一六国，都能车同轨书同文，你说你个太吾世界，咋就这么多非法出版社呢？\
本MOD可以自定义获取功法书的总纲、正逆练章节。\
源码：https://github.com/NacedWang/Taiwu_Same_Combat_Book_Type",
	Version = 2,
	Source = 1,
	Author = "Naced",
	DefaultSettings = 
	{
		[1] = 
		{
			DisplayName = "总纲类型",
			Description = "总纲类型",
			Key = "BookIndexType",
			Options = 
			{
				[1] = "承（刚正）",
				[2] = "合（任善）",
				[3] = "解（中庸）",
				[4] = "异（叛逆）",
				[5] = "独（唯我）"
			},
			SettingType = "Dropdown",
			DefaultValue = 2
		},
		[3] = 
		{
			DisplayName = "第一篇",
			Description = "第一篇类型",
			Key = "BookOneType",
			Options = 
			{
				[1] = "正",
				[2] = "逆"
			},
			SettingType = "Dropdown",
			DefaultValue = 0
		},
		[3] = 
		{
			DisplayName = "第二篇",
			Description = "第二篇类型",
			Key = "BookTwoType",
			Options = 
			{
				[1] = "正",
				[2] = "逆"
			},
			SettingType = "Dropdown",
			DefaultValue = 0
		},
		[4] = 
		{
			DisplayName = "第三篇",
			Description = "第三篇类型",
			Key = "BookThreeType",
			Options = 
			{
				[1] = "正",
				[2] = "逆"
			},
			SettingType = "Dropdown",
			DefaultValue = 0
		},
		[5] = 
		{
			DisplayName = "第四篇",
			Description = "第四篇类型",
			Key = "BookFourType",
			Options = 
			{
				[1] = "正",
				[2] = "逆"
			},
			SettingType = "Dropdown",
			DefaultValue = 0
		},
		[6] = 
		{
			DisplayName = "第五篇",
			Description = "第五篇类型",
			Key = "BookFiveType",
			Options = 
			{
				[1] = "正",
				[2] = "逆"
			},
			SettingType = "Dropdown",
			DefaultValue = 0
		},
		[7] = 
		{
			DisplayName = "书籍获取方式",
			Description = "书籍获取方式",
			Key = "BookGetType",
			Options = 
			{
				[1] = "额外获取",
				[2] = "替换原本"
			},
			SettingType = "Dropdown",
			DefaultValue = 0
		},
	},
	BackendPlugins = 
	{
		[1] = "SameCombatBookType.dll"
	},
	FileId = 2871831350,
	Cover = "logo.png",
	Title = "太吾出版社【功法书章节转化器】",
}
