# IMAG .NET source structure template

## Template

```
$/
  artifacts/
  build/
  docs/
  lib/
  packages/
  samples/
  src/
  tests/
  orthers/
  .editorconfig
  .gitignore
  .gitattributes
  build.cmd
  build.sh
  LICENSE
  NuGet.Config
  README.md
  {solution}.sln
```

- `src` - Main projects (the product code)

- `tests` - Test projects

- `docs` - Documentation stuff, markdown files, help files etc.

- `samples` (optional) - Sample projects

- `lib` - Things that can **NEVER** exist in a nuget package

- `artifacts` - Build outputs go here. Doing a build.cmd/build.sh generates artifacts here (nupkgs, dlls, pdbs, etc.)

- `packages` - NuGet packages

- `build` - Build customizations (custom msbuild files/psake/fake/albacore/etc) scripts

- `build.cmd` - Bootstrap the build for windows

- `build.sh` - Bootstrap the build for *nix

- `global.json` - ASP.NET vNext only

- `orthers/` - store other documents
  
  ## .gitignore
  
  ```
  [Oo]bj/
  [Bb]in/
  .nuget/
  _ReSharper.*
  packages/
  artifacts/
  *.user
  *.suo
  *.userprefs
  *DS_Store
  *.sln.ide
  ```
  
  There's probably more things that go in the ignore file.

- Update: Added docs folder

- Added README.md and LICENSE - Critical if you're OSS, if not ignore it

- Renamed `test` to `tests`

- Added lib for things that *CANNOT* exist in nuget packages

- Removed NuGet.config for people using packet :)

- Added global.json for ASP.NET vnext

- Added .editorconfig file in the root (x-plat IDE settings)

- Added NuGet.config back because people were confused about it missing

## "Script" for quick creation

Quickly create the project directory structure

### Win batch sctipt

### run

When creating a new project. To quickly create the project directory structure just copy the file NewNetStructure.cmd to the main directory and run CMD as follows

```batch
cmd NewNetStructure.cmd
```

### script

```batch
:: Chạy file này để tự động tạo thư mục và cấu trúc file
:: Kịch bản như sau: 
::   Tạo các thư mục sau nếu chưa tồn tại : 
::     artifacts, builddocs, lib, packages, samples, src, tests, orthers
::   Tạo các file sau nếu chưa tồn tại
::     .editorconfig, .gitignore, .gitattributes, build.cmd, build.sh, LICENSE, NuGet.Config, README.md


:: Main scripts
:: Tạo các thư mục sau nếu chưa tồn tại

::set folders[0]=(artifacts builddocs lib packages samples src tests orthers)
@echo off
setlocal enabledelayedexpansion

cls

echo IMAG .NET structure v0.0.0 (30/12/22)
echo -------------------------------------

echo:
echo 1 - Create Folders (artifacts, builddocs, lib, packages, samples, src, tests, orthers)
set folders[0]=artifacts
set folders[1]=build
set folders[1]=docs
set folders[2]=lib
set folders[3]=packages
set folders[4]=samples
set folders[5]=src
set folders[6]=tests
set folders[7]=orthers

:: FOR /L %%variable IN (lowerlimit,Increment,Upperlimit) DO do_something

for /l %%n in (0,1,7) do ( 
   IF not exist !folders[%%n]! (
   		echo Create new  [!folders[%%n]!] folder
   		mkdir !folders[%%n]!
   		if "!errorlevel!" EQU "0" (
    		echo --- Folder created successfully
  		) else (
    		echo --- Error while creating folder
  		)
   ) ELSE (
   		echo Folder [!folders[%%n]!] already exists
   )
)

:: tạo các file cần thiết
:: ( .editorconfig, .gitignore, .gitattributes, build.cmd, build.sh, LICENSE, NuGet.Config, README.md )
echo:
echo 1 - Create Files ( .editorconfig, .gitignore, .gitattributes, build.cmd, build.sh, LICENSE, NuGet.Config, README.md )

set files[0]=.editorconfig
set files[1]=.gitignore
set files[2]=.gitattributes
set files[3]=build.cmd
set files[4]=build.sh
set files[5]=LICENSE
set files[6]=NuGet.Config
set files[7]=README.md

for /l %%n in (0,1,7) do ( 
   IF not exist !files[%%n]! (
   		echo Create new [!files[%%n]!] file
   		echo '' >> !files[%%n]!
   		if "!errorlevel!" EQU "0" (
    		echo --- file created successfully
  		) else (
    		echo --- Error while creating file
  		)
   ) ELSE (
   		echo file [!files[%%n]!] already exists
   )
)

echo DONE
:: thêm nội dung cho file từng loại file
:: đang cập nhật
pause
exit
```

### result

```
IMAG .NET structure v0.0.0 (30/12/22)
-------------------------------------

1 - Create Folders (artifacts, builddocs, lib, packages, samples, src, tests, orthers)
Create new  [artifacts] folder
--- Folder created successfully
Create new  [docs] folder
--- Folder created successfully
Create new  [lib] folder
--- Folder created successfully
Create new  [packages] folder
--- Folder created successfully
Create new  [samples] folder
--- Folder created successfully
Create new  [src] folder
--- Folder created successfully
Create new  [tests] folder
--- Folder created successfully
Create new  [orthers] folder
--- Folder created successfully

1 - Create Files ( .editorconfig, .gitignore, .gitattributes, build.cmd, build.sh, LICENSE, NuGet.Config, README.md )
Create new [.editorconfig] file
--- file created successfully
Create new [.gitignore] file
--- file created successfully
Create new [.gitattributes] file
--- file created successfully
Create new [build.cmd] file
--- file created successfully
Create new [build.sh] file
--- file created successfully
Create new [LICENSE] file
--- file created successfully
Create new [NuGet.Config] file
--- file created successfully
file [README.md] already exists
DONE
Press any key to continue . . .
```

### linux

updatting ...
