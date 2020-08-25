import React, {Component} from 'react';

import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import IconButton from '@material-ui/core/IconButton';
import PublishIcon from '@material-ui/icons/Publish';

export class FileUpload extends Component {
    static displayName = FileUpload.name;

    constructor(props) {
        super(props);

        this.state = {
            file: '',
            fileName: 'Choose file',
            fileSelected: false,
            uploadMessage: '',
            uploadedFileDesciption: null,
            showUploader: false
        }
    }

    handleClose = () => this.setState({showUploader: false, fileSelected: false});
    handleShow = () => this.setState({showUploader: true});

    onFileSelect = e => {
        console.log(e.target.files);
        this.setState({
            file: e.target.files[0],
            fileName: e.target.files[0].name,
            fileSelected: true
        });
    }

    onSubmit = async e => {
        //e.PreventDefault();
        const formData = new FormData();
        formData.append('file', this.state.file);
        formData.append('path', this.props.dir);

        console.log(this.props.dir);

        await fetch('file/upload',
        {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + sessionStorage.tokenKey
            },
            body: formData
        })
        .then((response) => {
            if(!response.ok) throw new Error(response.status);
            else {
                console.log("jei");
                return response.json();
            }
        })
        .then((data) => {
            this.setState({
                uploadedFileDesciption: data
            })
        })
        .catch((error) => {
            console.error(error);
        });

        this.handleClose();

        this.props.callback();
    }

    render() {
        return(
            <>
            <IconButton onClick={this.handleShow}>
                <PublishIcon/>
            </IconButton>
            <Dialog open={this.state.showUploader} onClose={this.handleClose}>
                <DialogTitle>Upload File</DialogTitle>
                <DialogContent>
          <DialogContentText>
            To subscribe to this website, please enter your email address here. We will send updates
            occasionally.
          </DialogContentText>
          <TextField
            autoFocus
            onChange={this.onFileSelect}
            margin="dense"
            id="name"
            label={this.state.fileName}
            type="file"
            fullWidth
          />
        </DialogContent>
        <DialogActions>
          <Button color="primary" onClick={this.handleClose}>
            Cancel
          </Button>
          <Button color="primary" disabled={!this.state.fileSelected} type="submit" onClick={this.onSubmit}>
            Upload
          </Button>
        </DialogActions>
            </Dialog>
        </>
        )
    }
}
