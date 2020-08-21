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

  /*static renderForecastsTable(files, folders) {

    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Directory</th>
          </tr>
        </thead>
        <tbody>
          {folders.map(folder =>
            <tr key={folder.name} onClick={() => this.changeDirectory()}> 
              <td><BiFolder/>{folder.name}</td>
              <td>{folder.fullPath}</td>
            </tr>
          )}
          {files.map(file =>
            <tr key={file.fileName} onClick={() => downloadFile(file)}>
              <td><BiFileBlank/>{file.fileName}</td>
              <td>{file.directory}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }*/
  
  render() {
    var changeDirectory = this.changeDirectory;
    //let contents = Directory.renderForecastsTable(this.state.files, this.state.folders);

    async function downloadFile(file){
        
      const requestOptions = {
          method: 'POST',
          headers: {  
              //'Accept': 'application/octet-stream',
              'Content-Type': 'application/json'
          },
          //body: this.state.file
          body: JSON.stringify(file)
      };
  
      await fetch('filecontroller/download', requestOptions)
      .then((response) => response.blob())
      .then((blob) => {
          const url = window.URL.createObjectURL(new Blob([blob]));
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', file.fileName);
          // 3. Append to html page
          document.body.appendChild(link);
          // 4. Force download
          link.click();
          // 5. Clean up and remove the link
          link.parentNode.removeChild(link);
      })
    }

    return (
      <div>
        <h1 id="tabelLabel" >File System</h1>
        <p>This component demonstrates fetching data from the server.</p>
        <table className='table table-striped' aria-labelledby="tabelLabel">
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
            <tr key={file.fileName} onClick={() => downloadFile(file)}>
              <td><BiFileBlank/>{file.fileName}</td>
              <td>{file.directory}</td>
            </tr>
          )}
        </tbody>
      </table>
      </div>
    );
  }

  changeDirectory  = (folder) => {
    console.log("heifra innsiden");
    this.setState({
      currentDir : folder.fullPath
    });

    this.getDirectory();
  }

  async getDirectory() {
    const response = await fetch('filecontroller?' + new URLSearchParams({
      dir: this.state.currentDir,
  }));
    const data = await response.json();
    console.log(data);
    this.setState({
      files: data.files,
      folders: data.folders,
      loading: false 
    });
  }
}
