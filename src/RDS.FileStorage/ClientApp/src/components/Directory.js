import React, { Component } from 'react';
import { BiFolder, BiFileBlank } from "react-icons/bi";


export class Directory extends Component {
  static displayName = Directory.name;

  constructor(props) {
    super(props);
    this.state = {
      currentDir: "files/",
      folders : [],
      files : [], 
      loading: true 
    };

    this.changeDirectory = this.changeDirectory.bind(this);
  }

  componentDidMount() {
    this.getDirectory();
  }

  render() {
    var changeDirectory = this.changeDirectory;
    var downloadFile = this.downloadFile;

    return (
      <div>
        <h1 id="tabelLabel" >File System</h1>
        <p>This component demonstrates fetching data from the server.</p>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <th>Current Directory</th>
            <th>{this.state.currentDir}</th>
          </thead>
        <thead>
          <tr>
            <th>Name</th>
            <th>Directory</th>
          </tr>
        </thead>
        <tbody>
          {this.state.folders.map(folder =>
            <tr key={folder.name} onClick={() => changeDirectory(folder)}> 
              <td><BiFolder/>{folder.name}</td>
              <td>{folder.fullPath}</td>
            </tr>
          )}
          {this.state.files.map(file =>
            <tr key={file.name} onClick={() => downloadFile(file)}>
              <td><BiFileBlank/>{file.name}</td>
              <td>{file.directory}</td>
            </tr>
          )}
        </tbody>
      </table>
      </div>
    );
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
    .then((response) => response.blob())
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
  }

  changeDirectory  = (folder) => {
    console.log("heifra innsiden");
    this.setState({
      currentDir : folder.path
    }, () => {
      this.getDirectory()
    })
  }

  async getDirectory() {
    const folderReponse = await fetch('directory/folders?' + new URLSearchParams({
      dir: this.state.currentDir,
    }));
    const folders = await folderReponse.json();

    const fileResponse = await fetch('directory/files?' + new URLSearchParams({
      dir: this.state.currentDir,
    }));
    const files = await fileResponse.json();

    console.log(folders);
    console.log(files);

    this.setState({
      files: files,
      folders: folders,
      loading: false 
    });
  }
}
