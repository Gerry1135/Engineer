CS  = mcs
KSP = "$(HOME)/.local/share/Steam/SteamApps/common/Kerbal Space Program/KSP_Data/Managed/"
LIB = "Assembly-CSharp.dll" "Assembly-CSharp-firstpass.dll" "UnityEngine.dll"
LIB:= $(addprefix -r:$(KSP), $(LIB))
SRC = $(shell grep -o '<Compile Include="[^"]*' Engineer.csproj | sed -e 's/^[^"]*"//'  -e 's/\\/\//g')

all: Engineer.dll

Engineer.dll: $(SRC)
	@$(CS) -t:library $(LIB) $^ -out:$@

destroy:
	rm -f Engineer.dll

rebuild: destroy all
