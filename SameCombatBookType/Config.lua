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
		}
	},
	BackendPlugins = 
	{
		[1] = "SameCombatBookType.dll"
	},
	FileId = 2871831350,
	Cover = "logo.png",
	Title = "太吾出版社【功法书章节转化器】",
}