将大文件或目录打包、 压缩并分割成制定大小的文件，在Linux下可以通过组合使用tar,bzip2(或者gzip),split命令来实现。

命令格试 tar -zcvf [ file_directory ] |sqlit -b [ file_size ][ m,k ] - [ file.tar.gz ]

 

将file目录的文件压缩并分割成每个大小为4G文件

shell > tar -zcvf file_name |split -b 4096m - file_name.tar.gz

shell > ls

-rw-r--r-- 1 root root 4294967296 Mar  9 10:40 file_name.tar.gzaa
-rw-r--r-- 1 root root 4294967296 Mar  9 10:48 file_name.tar.gzab
-rw-r--r-- 1 root root 2282762240 Mar  9 10:52 file_name.tar.gzac



cat file_name.tar.gza* |tar zxv
解释：
用cat来读所有的压缩包，利用tar来进行解压



1、普通tar压缩命令

tar -zcvffile_name.tar.gzfile_name

//将file_name文件夹压缩成file_name.tar.gz



2、压缩后的文件太大，需要将cm-11.tar.gz分割成N个指定大小的文件，怎么办？一条命令搞定

split -b 4000M -d -a 1file_name.tar.gzfile_name.tar.gz.

//使用split命令，-b 4000M 表示设置每个分割包的大小，单位还是可以k

// -d "参数指定生成的分割包后缀为数字的形式

//-a x来设定序列的长度(默认值是2)，这里设定序列的长度为1


 执行命令后，生成压缩包如下：

-rw-r--r--  1 root     root      4194304000 May 20 14:00file_name.tar.gz.0
-rw-r--r--  1 root     root      4194304000 May 20 14:02 file_name.tar.gz.1
-rw-r--r--  1 root     root      4194304000 May 20 14:03 file_name.tar.gz.2
-rw-r--r--  1 root     root      4194304000 May 20 14:05 file_name.tar.gz.3
-rw-r--r--  1 root     root      4194304000 May 20 14:06 file_name.tar.gz.4
-rw-r--r--  1 root     root      4194304000 May 20 14:08 file_name.tar.gz.5
-rw-r--r--  1 root     root      4194304000 May 20 14:09 file_name.tar.gz.6
-rw-r--r--  1 root     root      2256379886 May 20 14:10 file_name.tar.gz.7



3、其实以上两步也可以合并成一步来执行

tar -zcvffile_name.tar.gzfile_name | split -b 4000M -d -a 1 -file_name.tar.gz.

//采用管道，其中 - 参数表示将所创建的文件输出到标准输出上



4、普通解压命令

tar -zxvf file_name.tar.gz



5、分割后的压缩包解压命令如下

catfile_name.tar.gz.* | tar -zxv



6、附上tar命令的参数解释


tar可以用来压缩打包单文件、多个文件、单个目录、多个目录。

Linux打包命令 tar

tar命令可以用来压缩打包单文件、多个文件、单个目录、多个目录。

常用格式：

单个文件压缩打包 tar -czvf my.tar.gz file1

多个文件压缩打包 tar -czvf my.tar.gz file1 file2,...（file*）（也可以给file*文件mv 目录在压缩）

单个目录压缩打包 tar -czvf my.tar.gz dir1

多个目录压缩打包 tar -czvf my.tar.gz dir1 dir2

解包至当前目录：tar -xzvf my.tar.gz

cpio

含子目录find x* | cpio -o > /y/z.cpio

不含子目录ls x* | cpio -o > /y/z.cpio

解包： cpio -i < /y/z.cpio

[root@linux ~]# tar [-cxtzjvfpPN] 文件与目录 ....

参数：

-c ：建立一个压缩文件的参数指令(create 的意思)；

-x ：解开一个压缩文件的参数指令！

-t ：查看 tarfile 里面的文件！

特别注意，在参数的下达中， c/x/t 仅能存在一个！不可同时存在！

因为不可能同时压缩与解压缩。

-z ：是否同时具有 gzip 的属性？亦即是否需要用 gzip 压缩？

-j ：是否同时具有 bzip2 的属性？亦即是否需要用 bzip2 压缩？

-v ：压缩的过程中显示文件！这个常用，但不建议用在背景执行过程！

-f ：使用档名，请留意，在 f 之后要立即接档名喔！不要再加参数！

　　　例如使用『 tar -zcvfP tfile sfile』就是错误的写法，要写成

　　　『 tar -zcvPf tfile sfile』才对喔！

-p ：使用原文件的原来属性（属性不会依据使用者而变）

-P ：可以使用绝对路径来压缩！

-N ：比后面接的日期(yyyy/mm/dd)还要新的才会被打包进新建的文件中！

--exclude FILE：在压缩的过程中，不要将 FILE 打包！