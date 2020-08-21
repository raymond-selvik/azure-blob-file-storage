import React, { Component } from 'react';
import {FileItem} from './FileItem';
import {DirItem} from './DirItem';


export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { 
      files: [],
      dirs: [], 
      loading: true 
    };
  }

  componentDidMount() {
    this.getFiles();
    this.getDirectories();
  }

  static renderForecastsTable(files, dirs) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Directory</th>
          </tr>
        </thead>
        <tbody>¨
          {dirs.map(dir =>
            <DirItem dir = {dir}/>
          )}
          {files.map(file =>
            <FileItem file = {file}/>
          )}
        </tbody>
      </table>
    );
  }
  
  render() {
    let contents = FetchData.renderForecastsTable(this.state.files, this.state.dirs);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async getFiles() {
    const response = await fetch('filecontroller/files');
    const data = await response.json();
    console.log(data);
    this.setState({ files: data, loading: false });
  }

  async getDirectories() {
    const response = await fetch('filecontroller/directories');
    const data = await response.json();
    console.log(data);
    this.setState({ dirs: data, loading: false });
  }
}
