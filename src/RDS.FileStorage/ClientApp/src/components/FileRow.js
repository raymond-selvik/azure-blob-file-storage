import React, {Component} from 'react';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import Menu from '@material-ui/core/Menu';
import DescriptionIcon from '@material-ui/icons/Description';
import MenuItem from '@material-ui/core/MenuItem';
import GetAppIcon from '@material-ui/icons/GetApp';
import DeleteForeverIcon from '@material-ui/icons/DeleteForever';
import CreateIcon from '@material-ui/icons/Create';

export class FileRow extends Component {
    
    constructor(props) {
        super(props);
        this.state = {
            file: this.props.file,
            open: false
        };
    }

    handleClick = (event) => {
        this.setState({open: true});
      };
    
    handleClose = () => {
        this.setState({open: false});
      };

      downloadFile = async () => {
        const requestOptions = {
            method: 'POST',
            headers: {  
                //'Accept': 'application/octet-stream',
                'Content-Type': 'application/json'
            },
            //body: this.state.file
            body: JSON.stringify(this.state.file)
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
            link.setAttribute('download', this.state.file.name);
            // 3. Append to html page
            document.body.appendChild(link);
            // 4. Force download
            link.click();
            // 5. Clean up and remove the link
            link.parentNode.removeChild(link);
            this.setState({open: false});
        })
        .catch( err => {
          alert('File not found');
        })
      }

    render() {
        return(
            <>
            <ListItem button onClick={() => this.handleClick()}>
                <ListItemIcon>
                <DescriptionIcon/>
            </ListItemIcon>
            <ListItemText>{this.state.file.name}</ListItemText>
            </ListItem>
            <Menu
                id="simple-menu"
                keepMounted
                getContentAnchorEl={null}
                anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
                transformOrigin={{ vertical: "top", horizontal: "center" }}
                onClose={this.handleClose}
                open={this.state.open}
                >
                <MenuItem onClick={this.downloadFile}><GetAppIcon/> Download</MenuItem>
                <MenuItem onClick={this.handleClose}><DeleteForeverIcon/> Delete</MenuItem>
                <MenuItem onClick={this.handleClose}><CreateIcon/> Rename</MenuItem>
            </Menu>
            </>
        )
    }
}