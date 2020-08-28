import React, { Component } from 'react';


import IconButton from '@material-ui/core/IconButton';
import ArrowLeftIcon from '@material-ui/icons/ArrowLeft';

import FolderIcon from '@material-ui/icons/Folder';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';
import ListSubheader from '@material-ui/core/ListSubheader';

import { FileUpload } from './FileUpload';
import { NewFolder } from './NewFolder';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import { FileRow } from './FileRow'

var path = require("path");

export class Directory extends Component {
  static displayName = Directory.name;

  constructor(props) {
    super(props);
    this.state = {
      currentDir: "/",
      folders : [],
      files : [], 
      loading: true ,
      selectedFile: '',
    };

    this.fileInput = React.fileInput;
    this.uploadFile = this.uploadFile.bind(this);

  }


  componentDidMount() {
    this.getDirectory(this.state.currentDir);
  }

  render() {
    var upDirectory = this.upDirectory;
    var downloadFile = this.downloadFile;
    var uploadFile = this.uploadFile;
    var getDirectory = this.getDirectory;
    var changeDirectory = this.changeDirectory

 
    
    return (
      <div>
        <h1 id="tabelLabel" >File System</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <>
          <NewFolder
            dir={this.state.currentDir}
            callback={this.refreshDirectory}
          />
    
        
          <FileUpload
            dir={this.state.currentDir}
            callback={this.refreshDirectory}></FileUpload>
        </>
        <Divider/>
        <List component="nav" subheader={<ListSubheader><IconButton onClick={() => this.upDirectory()}><ArrowLeftIcon/></IconButton>{this.state.currentDir}</ListSubheader>} dense={true}>
        <Divider/>
        {this.state.folders.map(folder =>
        <>
          <ListItem button onClick={() => this.changeDirectory(folder)}>
            <ListItemIcon>
              <FolderIcon/>
            </ListItemIcon>
            <ListItemText>{folder.name}</ListItemText>
          </ListItem>
          <Divider/>
          </>
          )}
        {this.state.files.map(file =>
        <>
          <FileRow file={file}/>
          <Divider/>
          </>
          )}
        </List>
      </div>
    );
  }

  async uploadFile (event) {
    this.setState({selectedFile: event.target.value});
    console.log(event.target.files);

    let form = new FormData();

    form.append('file', event.target.files[0]);
    form.append('path', this.state.currentDir);

    await fetch('file/upload',
        {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + sessionStorage.tokenKey
            },
            body: form
        });
  }

  downloadFile = async (file) => {
        
    const requestOptions = {
        method: 'POST',
        headers: {  
            //'Accept': 'application/octet-stream',
            'Content-Type': 'application/json'
        },
        //body: this.state.file
        body: JSON.stringify(file)
    };

    await fetch('file/download', requestOptions)
    .then((response) => {
        if (!response.ok) {
          throw response;
        }

        return response.blob();
      })
    .then((blob) => {
        const url = window.URL.createObjectURL(new Blob([blob]));
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', file.name);
        // 3. Append to html page
        document.body.appendChild(link);
        // 4. Force download
        link.click();
        // 5. Clean up and remove the link
        link.parentNode.removeChild(link);
    })
    .catch( err => {
      alert('File not found');
    })
  }

  upDirectory = () => {
    console.log(this.state.currentDir);
    this.getDirectory(this.state.currentDir + "/..");
  }

  changeDirectory = (folder) => {
    console.log(folder.fullPath);
    this.getDirectory(folder.fullPath);
  }

  refreshDirectory = () => {
    this.getDirectory(this.state.currentDir);
  }

  getDirectory = async (dir) => {
    const folderReponse = await fetch('directory/folders?' + new URLSearchParams({
      dir: dir
    }));
    const folders = await folderReponse.json();

    const fileResponse = await fetch('directory/files?' + new URLSearchParams({
      dir: dir
    }));
    const files = await fileResponse.json();

    console.log(folders);
    console.log(files);
    console.log(dir);
    var newDir = path.normalize(dir)

    this.setState({
      currentDir: newDir,
      files: files,
      folders: folders,
      loading: false 
    });
  }
}
